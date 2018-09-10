using cloudscribe.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace cloudscribe.Core.Services.GraphQL
{
    public class SiteService
    {
        public SiteService(
            IServiceProvider serviceProvider,
            //ISiteQueries siteQueries,
            //ISiteCommands siteCommands,
            ILogger<SiteService> logger
            )
        {
            _serviceProvider = serviceProvider;
            // _siteQueries = siteQueries;
            //_siteCommands = siteCommands;

            _log = logger;

        }

        private readonly IServiceProvider _serviceProvider;

        //private readonly ISiteQueries _siteQueries;
        //private readonly ISiteCommands _siteCommands;
        private readonly ILogger _log;

        public async Task<List<ISiteInfo>> GetAllSites(CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var queries = scopedServices.GetService<ISiteQueries>();
                return await queries.GetList(cancellationToken).ConfigureAwait(false);

            }      
        }

        public async Task<ISiteSettings> GetSite(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var queries = scopedServices.GetService<ISiteQueries>();
                return await queries.Fetch(id, cancellationToken).ConfigureAwait(false);

            }
        }
    }
}
