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
            services.AddSingleton<SiteUpdatedEvent>();
            services.AddSingleton<SiteInfoType>();
            services.AddSingleton<SiteUpdateInputType>();

            services.AddSingleton<CompanyInfoUpdateType>();

            services.AddSingleton<IGraphQueryMarker, CoreQuery>();
            services.AddSingleton<IGraphMutationMarker, CoreMutation>();
            services.AddSingleton<IGraphSubscriptionMarker, CoreSubscription>();

            services.AddSingleton<SiteService>();



            return services;
        }

    }
}
