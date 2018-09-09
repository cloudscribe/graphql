using cloudscribe.Core.Models.GraphQL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddGraphQLModelsForCloudscribeCore(this IServiceCollection services)
        {
            services.AddSingleton<SiteType>();
            services.AddSingleton<SiteInfoType>();


            return services;
        }

    }
}
