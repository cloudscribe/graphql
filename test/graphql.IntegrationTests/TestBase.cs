using graphql.WebApp;
using GraphQL.Client.Http;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
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

        public HttpMessageHandler GetHandler()
        {
            return _factory.Server.CreateHandler();
        }

        public async Task<GraphQLHttpClient> GetAuthenticatedGraphQLClient(string userName = "admin@admin.com", string password = "admin")
        {
            var client = await GetAuthenticatedClient(userName, password);
            var clientOptions = new GraphQLHttpClientOptions
            {
                HttpMessageHandler = GetHandler(),
                EndPoint = new Uri("http://localhost/graphql")
            };
            return client.AsGraphQLClient(clientOptions);

        }

        public GraphQLHttpClient GetUnauthenticatedGraphQLClient()
        {
            var client = _factory.CreateClient();
            var clientOptions = new GraphQLHttpClientOptions
            {
                HttpMessageHandler = GetHandler(),
                EndPoint = new Uri("http://localhost/graphql")
            };
            return client.AsGraphQLClient(clientOptions);
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
