using cloudscribe.Extensions.GraphQL;
using cloudscribe.SimpleContent.GraphQL.GraphTypes;
using cloudscribe.SimpleContent.GraphQL.Services;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace cloudscribe.SimpleContent.GraphQL
{
    public class SimpleContentQuery : ObjectGraphType<object>, IGraphQueryMarker
    {
        public SimpleContentQuery(PageService pageService)
        {
            Name = "SimpleContentQuery";

            FieldAsync<PageType>(
                "getPageFromSlug",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "projectId", Description = "The project id, same as siteid if using cloudscribe core" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "slug", Description = "The url slug for the page" }

                ),
                resolve: async context =>
                {
                    var projectId = context.GetArgument<string>("projectId");
                    var slug = context.GetArgument<string>("slug");
                    return await context.TryAsyncResolve(
                        async c => await pageService.GetPageBySlug(projectId, slug, c.CancellationToken).ConfigureAwait(false)
                        );

                }
            );
        }
    }
}
