using cloudscribe.Core.Services.GraphQL;
using cloudscribe.Extensions.GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace cloudscribe.Core.Models.GraphQL
{
    public class CoreQuery : ObjectGraphType<object>, IGraphQueryMarker
    {
        public CoreQuery(
            SiteService siteService
            )
        {
            Name = "CoreQuery";

            FieldAsync<ListGraphType<SiteInfoType>>(
                "sites",
                resolve: async context =>
                {
                    return await context.TryAsyncResolve(async c => await siteService.GetAllSites(c.CancellationToken));
                }
            );


            FieldAsync<SiteType>(
                "site",
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
        }
    }
}
