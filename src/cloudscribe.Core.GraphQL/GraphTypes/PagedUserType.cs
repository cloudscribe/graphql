using cloudscribe.Core.GraphQL.Services;
using cloudscribe.Core.Models;
using cloudscribe.Pagination.Models;
using GraphQL.Authorization.AspNetCore;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace cloudscribe.Core.GraphQL.GraphTypes
{
    public class PagedUserType : ObjectGraphType<PagedResult<IUserInfo>>
    {
        public PagedUserType(SiteService siteService)
        {
            Field(x => x.PageNumber, type: typeof(IntGraphType)).Description("The current page number");
            Field(x => x.PageSize, type: typeof(IntGraphType)).Description("The page size");
            Field(x => x.TotalItems, type: typeof(IntGraphType)).Description("The total number of items");

            FieldAsync<ListGraphType<SiteUserInfoType>>(
                "list",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id", Description = "id of the site" }
                ),
                resolve: async context =>
                {
                    return await context.TryAsyncResolve(async c => await siteService.GetAllSites(c.CancellationToken));
                }
            )
            .AuthorizeWith("AdminPolicy")
            ;

        }
    }
}
