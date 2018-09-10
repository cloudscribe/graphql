﻿using cloudscribe.Core.Services.GraphQL;
using cloudscribe.Extensions.GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace cloudscribe.Core.Models.GraphQL
{
    public class CoreMutation : ObjectGraphType, IGraphMutationMarker
    {
        public CoreMutation(
            SiteService siteService
            )
        {
            Name = "CoreMutation";

            FieldAsync<SiteType>(
                "updateSiteName",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id", Description = "id of the site" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "newName", Description = "The new name for the site" }
                ),
                resolve: async context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    var newName = context.GetArgument<string>("newName");
                    return await context.TryAsyncResolve(
                        async c => await siteService.UpdateSiteName(id, newName)
                    );

                }

            );

        }
    }
}