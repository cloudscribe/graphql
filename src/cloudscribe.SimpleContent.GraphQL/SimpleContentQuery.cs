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
        public SimpleContentQuery(
            PageService pageService,
            PostService postservice
            )
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

            FieldAsync<PagedPostType>(
                "postList",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "projectId", Description = "The project id, same as siteid if using cloudscribe core" },
                    new QueryArgument<StringGraphType> { Name = "category", Description = "Optional category filter" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "pageNumber", Description = "The page number to get" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "pageSize", Description = "The number of posts per page" }
                   // new QueryArgument<NonNullGraphType<BooleanGraphType>> { Name = "includeUnpublished", Description = "Should only pass true for users with edit permission" }

                ),
                resolve: async context =>
                {
                    var projectId = context.GetArgument<string>("projectId");
                    var category = context.GetArgument<string>("category");
                    var pageNumber = context.GetArgument<int>("pageNumber");
                    var pageSize = context.GetArgument<int>("pageSize");
                    var includeUnpublished = false;

                    return await context.TryAsyncResolve(
                        async c => await postservice.GetPosts(projectId, 
                             category, 
                             pageNumber,
                             pageSize,
                             includeUnpublished,
                             c.CancellationToken).ConfigureAwait(false)
                        );

                }
            );
        }
    }
}
