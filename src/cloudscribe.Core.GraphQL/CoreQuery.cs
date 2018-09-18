using cloudscribe.Core.GraphQL.GraphTypes;
using cloudscribe.Core.GraphQL.Services;
using cloudscribe.Extensions.GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;
using GraphQL.Authorization.AspNetCore;

namespace cloudscribe.Core.GraphQL
{
    public class CoreQuery : ObjectGraphType<object>, IGraphQueryMarker
    {
        public CoreQuery(
            SiteService siteService
            )
        {
            Name = "CoreQuery";

            FieldAsync<ListGraphType<SiteInfoType>>(
                "siteList",
                resolve: async context =>
                {
                    return await context.TryAsyncResolve(async c => await siteService.GetAllSites(c.CancellationToken));
                }
            )
            .AuthorizeWith("AdminPolicy")
            ;


            FieldAsync<SiteType>(
                "siteFromId",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id", Description = "id of the site" }
                ),
                resolve: async context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    return await context.TryAsyncResolve(
                        async c => await siteService.GetSite(id, c.CancellationToken)
                    );
                    
                }
            );

            FieldAsync<SiteType>(
                "siteFromRequest",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "hostName", Description = "The host name corresponding to the site" },
                    new QueryArgument<StringGraphType> { Name = "firstFolderSegment", Description = "the first folder segment for resolving folder based sites" }

                ),
                resolve: async context =>
                {
                    var hostName = context.GetArgument<string>("hostName");
                    var firstFolderSegment = context.GetArgument<string>("firstFolderSegment");
                return await context.TryAsyncResolve(
                    async c => await siteService.GetSite(hostName, firstFolderSegment, c.CancellationToken).ConfigureAwait(false)
                    );

                }
            );

        }
    }
}
