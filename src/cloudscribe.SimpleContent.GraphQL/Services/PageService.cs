using cloudscribe.SimpleContent.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace cloudscribe.SimpleContent.GraphQL.Services
{
    public class PageService
    {
        public PageService(
            IServiceProvider serviceProvider,
            ILogger<PageService> logger
            )
        {
            _serviceProvider = serviceProvider;
            _log = logger;
        }

        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _log;

        public async Task<IPage> GetPageBySlug(string projectId, string slug, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var queries = scopedServices.GetService<IPageQueries>();
                return await queries.GetPageBySlug(projectId, slug, cancellationToken).ConfigureAwait(false);
                
            }
        }

    }
}
