﻿using GraphQL.Language.AST;
using GraphQL.Types;
using GraphQL.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Authorization.AspNetCore
{
    public class AuthorizationValidationRule : IValidationRule
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly AuthorizationValidationOptions _options;

        public AuthorizationValidationRule(
            IOptions<AuthorizationValidationOptions> optionsAccessor,
            IAuthorizationService authorizationService
            )
        {
            _authorizationService = authorizationService;
            _options = optionsAccessor.Value;
        }

        public INodeVisitor Validate(ValidationContext context)
        {
            var userContext = context.UserContext as IProvideClaimsPrincipal;

            return new EnterLeaveListener(_ =>
            {
                var operationType = OperationType.Query;

                // this could leak info about hidden fields or types in error messages
                // it would be better to implement a filter on the Schema so it
                // acts as if they just don't exist vs. an auth denied error
                // - filtering the Schema is not currently supported

                _.Match<Operation>(astType =>
                {
                    operationType = astType.OperationType;

                    var type = context.TypeInfo.GetLastType();
                    AuthorizeAsync(astType, type, userContext, context, operationType).GetAwaiter().GetResult();
                });

                _.Match<Field>(fieldAst =>
                {
                    var fieldDef = context.TypeInfo.GetFieldDef();
                    // check target field
                    AuthorizeAsync(fieldAst, fieldDef, userContext, context, operationType).GetAwaiter().GetResult();
                    // check returned graph type
                    AuthorizeAsync(fieldAst, fieldDef.ResolvedType, userContext, context, operationType).GetAwaiter().GetResult();
                });
            });
        }

        private async Task AuthorizeAsync(
            INode node,
            IProvideMetadata type,
            IProvideClaimsPrincipal userContext,
            ValidationContext context,
            OperationType operationType)
        {
            if (userContext == null)
            {
                throw new ArgumentNullException(
                    nameof(userContext),
                    $"You must register a user context that implements {nameof(IProvideClaimsPrincipal)}.");
            }

            if (type == null || !type.RequiresAuthorization())
            {
                return;
            }

            var policyNames = type.GetPolicies();
            if (policyNames.Count == 0)
            {
                return;
            }

            var tasks = new List<Task<AuthorizationResult>>(policyNames.Count);
            foreach (var policyName in policyNames)
            {
                var task = _authorizationService.AuthorizeAsync(userContext?.User, policyName);
                tasks.Add(task);
            }
            await Task.WhenAll(tasks);

            foreach (var task in tasks)
            {
                var result = task.Result;
                if (!result.Succeeded)
                {
                    var stringBuilder = new StringBuilder("You are not authorized to run this ");
                    stringBuilder.Append(operationType.ToString().ToLower());
                    stringBuilder.AppendLine(".");

                    if(_options.IncludeErrorDetails)
                    {
                        foreach (var failure in result.Failure.FailedRequirements)
                        {
                            AppendFailureLine(stringBuilder, failure);
                        }
                    }
                   
                    context.ReportError(
                        new ValidationError(context.OriginalQuery, "authorization", stringBuilder.ToString(), node));
                }
            }
        }

        private static void AppendFailureLine(
            StringBuilder stringBuilder,
            IAuthorizationRequirement authorizationRequirement)
        {
            switch (authorizationRequirement)
            {
                case ClaimsAuthorizationRequirement claimsAuthorizationRequirement:
                    stringBuilder.Append("Required claim '");
                    stringBuilder.Append(claimsAuthorizationRequirement.ClaimType);
                    if (claimsAuthorizationRequirement.AllowedValues == null || !claimsAuthorizationRequirement.AllowedValues.Any())
                    {
                        stringBuilder.AppendLine("' is not present.");
                    }
                    else
                    {
                        stringBuilder.Append("' with any value of '");
                        stringBuilder.Append(string.Join(", ", claimsAuthorizationRequirement.AllowedValues));
                        stringBuilder.AppendLine("' is not present.");
                    }
                    break;
                case DenyAnonymousAuthorizationRequirement denyAnonymousAuthorizationRequirement:
                    stringBuilder.AppendLine("The current user must be authenticated.");
                    break;
                case NameAuthorizationRequirement nameAuthorizationRequirement:
                    stringBuilder.Append("The current user name must match the name '");
                    stringBuilder.Append(nameAuthorizationRequirement.RequiredName);
                    stringBuilder.AppendLine("'.");
                    break;
                case OperationAuthorizationRequirement operationAuthorizationRequirement:
                    stringBuilder.Append("Required operation '");
                    stringBuilder.Append(operationAuthorizationRequirement.Name);
                    stringBuilder.AppendLine("' was not present.");
                    break;
                case RolesAuthorizationRequirement rolesAuthorizationRequirement:
                    if (rolesAuthorizationRequirement.AllowedRoles == null || !rolesAuthorizationRequirement.AllowedRoles.Any())
                    {
                        // This should never happen.
                        stringBuilder.AppendLine("Required roles are not present.");
                    }
                    else
                    {
                        stringBuilder.Append("Required roles '");
                        stringBuilder.Append(string.Join(", ", rolesAuthorizationRequirement.AllowedRoles));
                        stringBuilder.AppendLine("' are not present.");
                    }
                    break;
                default:
                    stringBuilder.Append("Requirement '");
                    stringBuilder.Append(authorizationRequirement.GetType().Name);
                    stringBuilder.AppendLine("' was not satisfied.");
                    break;
            }
        }
    }
}
