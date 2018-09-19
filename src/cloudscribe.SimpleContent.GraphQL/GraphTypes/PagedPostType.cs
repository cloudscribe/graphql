using cloudscribe.Pagination.Models;
using cloudscribe.SimpleContent.Models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace cloudscribe.SimpleContent.GraphQL.GraphTypes
{
    public class PagedPostType : ObjectGraphType<PagedResult<IPost>>
    {
        public PagedPostType()
        {
            Field(x => x.PageNumber, type: typeof(IntGraphType)).Description("The current page number");
            Field(x => x.PageSize, type: typeof(IntGraphType)).Description("The page size");
            Field(x => x.TotalItems, type: typeof(IntGraphType)).Description("The total number of items");
            Field<ListGraphType<PostType>>("posts",
                resolve: context => context.Source.Data
                );
        }
    }
}
