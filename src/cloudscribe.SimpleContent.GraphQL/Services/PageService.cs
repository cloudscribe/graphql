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
            IPageQueriesSingleton pageQueries,
            ILogger<PageService> logger
            )
        {
            _pageQueries = pageQueries;
            _log = logger;
        }

        private readonly IPageQueriesSingleton _pageQueries;
        private readonly ILogger _log;

        public async Task<IPage> GetPageBySlug(string projectId, string slug, CancellationToken cancellationToken = default(CancellationToken))
        {
           
            return await _pageQueries.GetPageBySlug(projectId, slug, cancellationToken).ConfigureAwait(false);
               
        }

    }
}
