using cloudscribe.Core.ApiModels;
using cloudscribe.Extensions.Blazor.Config;
using cloudscribe.Extensions.Blazor.GraphQL;
using cloudscribe.Extensions.Blazor.Oidc;
using GraphQL.Common.Request;
using System.Net.Http;
using System.Threading.Tasks;

namespace cloudscribe.Core.Blazor
{
    public class SiteContextService
    {
        public SiteContextService(
            OidcService oidcService,
            HttpClient httpClient
            )
        {
            _oidcService = oidcService;
            _httpClient = httpClient;
        }

        #region Private

        private readonly OidcService _oidcService;
        private readonly HttpClient _httpClient;
        
        private async Task EnsureSite()
        {
            if(CurrentSite != null) { return; }

            var siteId = await AppSettings.Get("siteId");

            var query = new GraphQLRequest
            {
                Query = GraphQLQueryConstants.SiteContextQuery
            };

            query.Variables = new
            {
               siteId
            };

            await _httpClient.AddBeaerTokenIfAuthenticated(_oidcService);
            var endPoint = await AppSettings.Get("graphqlApiUri");

            var result = await _httpClient.SendQuery<SiteContextModel>(endPoint, query);
            if (result.SuccessResult != null)
            {
                CurrentSite = result.SuccessResult.Data.Site;
               
            }
            else if (result.Errors != null)
            {
                Errors = result.Errors;
            }

        }


        class SiteContextModel
        {
            public DataResult Data { get; set; }

            public class DataResult
            {
                public SiteDescriptor Site { get; set; }
            }
        }

        #endregion


        public SiteDescriptor CurrentSite { get; private set; } = null;

        public async Task Init()
        {
            await EnsureSite();
        }

        public ErrorModel Errors { get; private set; } = null;


    }
}
