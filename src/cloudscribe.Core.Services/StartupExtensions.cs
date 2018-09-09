using cloudscribe.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddServicesForCloudscribeCore(this IServiceCollection services)
        {
            services.AddSingleton<SiteService>();


            return services;
        }

    }
}
