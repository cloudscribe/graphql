using cloudscribe.Core.GraphQL;
using cloudscribe.Core.GraphQL.GraphTypes;
using cloudscribe.Core.GraphQL.Services;
using cloudscribe.Extensions.GraphQL;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddGraphQLForCloudscribeCore(this IServiceCollection services)
        {
            services.AddSingleton<SiteType>();
            services.AddSingleton<SiteInfoType>();
            services.AddSingleton<SiteUpdateInputType>();

            services.AddSingleton<IGraphQueryMarker, CoreQuery>();
            services.AddSingleton<IGraphMutationMarker, CoreMutation>();

            services.AddSingleton<SiteService>();



            return services;
        }

    }
}
