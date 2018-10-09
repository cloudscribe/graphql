using cloudscribe.Core.ApiModels;
using cloudscribe.Core.Models;
using cloudscribe.Core.Web.Components;
using cloudscribe.Extensions.GraphQL;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace cloudscribe.Core.GraphQL.Services
{
    public class SiteService
    {
        public SiteService(
            ISiteQueriesSingleton siteQueries,
            ISiteCommandsSingleton siteCommands,
            IServiceProvider serviceProvider,
            ILogger<SiteService> logger
            )
        {
            _siteQueries = siteQueries;
            _siteCommands = siteCommands;
            _serviceProvider = serviceProvider;
            _log = logger;
            whenSiteUpdated = new Subject<SiteSettings>();
        }

        private readonly ISiteQueriesSingleton _siteQueries;
        private readonly ISiteCommandsSingleton _siteCommands;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _log;

        private readonly Subject<SiteSettings> whenSiteUpdated;

        public IObservable<SiteSettings> WhenSiteUpdated => this.whenSiteUpdated.AsObservable();

        public async Task<List<ISiteInfo>> GetAllSites(CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var queries = scopedServices.GetService<ISiteQueries>();
                return await queries.GetList(cancellationToken).ConfigureAwait(false);

            }      
        }

        public async Task<ISiteContext> GetSite(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var site = await _siteQueries.Fetch(id, cancellationToken).ConfigureAwait(false);
            return site as ISiteContext;
        }

        public async Task<ISiteContext> GetSite(string hostName, string firstFolderSegment, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var siteResolver = scopedServices.GetService<ISiteContextResolver>();
                return await siteResolver.ResolveSite(hostName, firstFolderSegment, cancellationToken).ConfigureAwait(false);

            }
        }


        public async Task<ISiteSettings> UpdateSiteName(Guid id, string newName, CancellationToken cancellationToken = default(CancellationToken))
        {
            
            var site = await _siteQueries.Fetch(id, cancellationToken).ConfigureAwait(false);
            site.SiteName = newName;
            await _siteCommands.Update(site).ConfigureAwait(false);

            return site;
            
        }

        public async Task<ISiteSettings> UpdateSite(Guid id, SiteUpdateModel patch, CancellationToken cancellationToken = default(CancellationToken))
        {
           
            var site = await _siteQueries.Fetch(id, cancellationToken).ConfigureAwait(false);

            if(!string.IsNullOrEmpty(patch.SiteName))
            {
                site.SiteName = patch.SiteName;
            }
            if(patch.AllowPersistentLogin.HasValue)
            {
                site.AllowPersistentLogin = patch.AllowPersistentLogin.Value;
            }
                
            await _siteCommands.Update(site).ConfigureAwait(false);

            return site;
            
        }

        public async Task<ISiteSettings> UpdateSite(Guid id, Dictionary<string, object> patch, CancellationToken cancellationToken = default(CancellationToken))
        {
           
            var site = await _siteQueries.Fetch(id, cancellationToken).ConfigureAwait(false);
            patch.ApplyPatch<CompanyInfoUpdateModel, ISiteSettings>(site);
            await _siteCommands.Update(site).ConfigureAwait(false);

            var sc = site as SiteSettings;

            whenSiteUpdated.OnNext(sc);
                
            return site;
            
        }

    }
}
