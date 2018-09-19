using System;
using System.Collections.Generic;
using System.Text;
using cloudscribe.SimpleContent.GraphQL;
using cloudscribe.Extensions.GraphQL;
using cloudscribe.SimpleContent.GraphQL.Services;
using cloudscribe.SimpleContent.GraphQL.GraphTypes;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddGraphQLForCloudscribeSimpleContent(this IServiceCollection services)
        {
            services.AddSingleton<PageType>();
            services.AddSingleton<PostType>();
            //services.AddSingleton<SiteUpdateInputType>();

            services.AddSingleton<IGraphQueryMarker, SimpleContentQuery>();
            //services.AddSingleton<IGraphMutationMarker, SimpleContentMutation>();

            services.AddSingleton<PageService>();



            return services;
        }
    }
}
