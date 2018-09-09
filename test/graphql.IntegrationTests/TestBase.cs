using graphql.WebApp;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace graphql.IntegrationTests
{
    public class TestBase : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public TestBase(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        public HttpClient GetUnauthenticatedClient()
        {
            return _factory.CreateClient();
        }

        public async Task<HttpClient> GetAuthenticatedClient(string userName = "admin@admin.com", string password = "admin")
        {
            var client = _factory.CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                });

            var invoker = client as HttpMessageInvoker;
            var tokenRequest = new PasswordTokenRequest
            {
                Address = "http://localhost/connect/token",
                ClientId = "Test",
                ClientSecret = "secret",
                Scope = "irapi",
                UserName = userName,
                Password = password
            };

            var tokenResponse = await invoker.RequestPasswordTokenAsync(tokenRequest);

            client.SetBearerToken(tokenResponse.AccessToken);
            return client;
        }

    }
}
