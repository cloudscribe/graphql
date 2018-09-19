using cloudscribe.Extensions.GraphQL;
using cloudscribe.SimpleContent.GraphQL;
using cloudscribe.SimpleContent.GraphQL.GraphTypes;
using cloudscribe.SimpleContent.GraphQL.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddGraphQLForCloudscribeSimpleContent(this IServiceCollection services)
        {
            services.AddSingleton<PageType>();
            services.AddSingleton<PostType>();
            services.AddSingleton<PagedPostType>();
            //services.AddSingleton<SiteUpdateInputType>();

            services.AddSingleton<IGraphQueryMarker, SimpleContentQuery>();
            //services.AddSingleton<IGraphMutationMarker, SimpleContentMutation>();

            services.AddSingleton<PageService>();
            services.AddSingleton<PostService>();



            return services;
        }
    }
}
