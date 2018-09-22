using cloudscribe.Core.ApiModels;
using cloudscribe.Core.Models;
using cloudscribe.Core.Web.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace cloudscribe.Core.GraphQL.Services
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

        public async Task<ISiteContext> GetSite(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var queries = scopedServices.GetService<ISiteQueries>();
                var site = await queries.Fetch(id, cancellationToken).ConfigureAwait(false);
                return site as ISiteContext;

            }
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
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var queries = scopedServices.GetService<ISiteQueries>();
                var commands = scopedServices.GetService<ISiteCommands>();
                var site = await queries.Fetch(id, cancellationToken).ConfigureAwait(false);
                site.SiteName = newName;
                await commands.Update(site).ConfigureAwait(false);

                return site;

            }
        }

        public async Task<ISiteSettings> UpdateSite(Guid id, SiteUpdateModel patch, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var queries = scopedServices.GetService<ISiteQueries>();
                var commands = scopedServices.GetService<ISiteCommands>();
                var site = await queries.Fetch(id, cancellationToken).ConfigureAwait(false);
                //site.SiteName = newName;
                if(!string.IsNullOrEmpty(patch.SiteName))
                {
                    site.SiteName = patch.SiteName;
                }
                if(patch.AllowPersistentLogin.HasValue)
                {
                    site.AllowPersistentLogin = patch.AllowPersistentLogin.Value;
                }
                

                await commands.Update(site).ConfigureAwait(false);

                return site;

            }
        }

        public async Task<ISiteSettings> UpdateSite(Guid id, Dictionary<string, object> patch, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var queries = scopedServices.GetService<ISiteQueries>();
                var commands = scopedServices.GetService<ISiteCommands>();
                var site = await queries.Fetch(id, cancellationToken).ConfigureAwait(false);

                var model = new CompanyInfoUpdateModel();
                var modelProps = model.GetType().GetProperties();
                var siteProps = site.GetType().GetProperties();

                var comparer = StringComparer.OrdinalIgnoreCase;
                var caseInsensitivePatch = new Dictionary<string, object>(patch, comparer);

                foreach (var prop in modelProps)
                {
                    foreach(var siteProp in siteProps)
                    {
                        if(siteProp.Name == prop.Name)
                        {
                            if(caseInsensitivePatch.ContainsKey(siteProp.Name))
                            {
                                var newValue = caseInsensitivePatch[siteProp.Name];
                                siteProp.SetValue(site, Convert.ChangeType(newValue, siteProp.PropertyType), null); 
                            }
                        }
                    }
  
                }
                
                await commands.Update(site).ConfigureAwait(false);

                return site;

            }
        }

        //public async Task<ISiteSettings> UpdateSite(Guid id, CompanyInfoUpdateModel patch, CancellationToken cancellationToken = default(CancellationToken))
        //{
        //    using (var scope = _serviceProvider.CreateScope())
        //    {
        //        var scopedServices = scope.ServiceProvider;
        //        var queries = scopedServices.GetService<ISiteQueries>();
        //        var commands = scopedServices.GetService<ISiteCommands>();
        //        var site = await queries.Fetch(id, cancellationToken).ConfigureAwait(false);

        //        site.CompanyCountry = patch.CompanyCountry;
        //        site.CompanyFax = patch.CompanyFax;
        //        site.CompanyLocality = patch.CompanyLocality;
        //        site.CompanyName = patch.CompanyName;
        //        site.CompanyPhone = patch.CompanyPhone;
        //        site.CompanyPostalCode = patch.CompanyPostalCode;
        //        site.CompanyPublicEmail = patch.CompanyPublicEmail;
        //        site.CompanyRegion = patch.CompanyRegion;
        //        site.CompanyStreetAddress = patch.CompanyStreetAddress;
        //        site.CompanyStreetAddress2 = patch.CompanyStreetAddress2;
        //        site.CompanyWebsite = patch.CompanyWebsite;


        //        await commands.Update(site).ConfigureAwait(false);

        //        return site;

        //    }
        //}

    }
}
