using GraphQL.Authorization.AspNetCore;
using GraphQL.Server.Transports.Subscriptions.Abstractions;
using GraphQL.Validation;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGraphQLAuthorization(
            this IServiceCollection services,
            bool includeAuthorizationErrorDetails = false
            )
        {
            services.Configure<AuthorizationValidationOptions>(options =>
            {
                options.IncludeErrorDetails = includeAuthorizationErrorDetails;
            });

            services.AddTransient<IValidationRule, AuthorizationValidationRule>();
            services.AddTransient<IOperationMessageListener, AddUserContextMessageListener>();
            services.TryAddTransient<IWebSocketRequestAuthorizationPreHandler, NoopWebSocketRequestAuthorizationPreHandler>();

            return services;
        }

    }
}
