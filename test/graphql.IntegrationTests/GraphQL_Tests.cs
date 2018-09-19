using cloudscribe.Core.Models;
using sourceDev.WebApp;
using GraphQL.Common.Request;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Net;
using cloudscribe.Core.ApiModels;

namespace graphql.IntegrationTests
{
    public class GraphQL_Tests : TestBase
    {
        public GraphQL_Tests(CustomWebApplicationFactory<Startup> factory):base(factory)
        {

        }

        //[Fact]
        //public async Task T10000_ReturnOkForSitesQueryWithHttpClient()
        //{
        //    // Arrange
        //    const string query = @"{
        //        ""query"": ""query { siteList { id, aliasId, siteName, siteFolderName } }""
        //    }";
        //    var content = new StringContent(query, Encoding.UTF8, "application/json");

        //    // Act
        //    var _client = GetUnauthenticatedClient();
        //    var response = await _client.PostAsync("/graphql", content);

        //    response.EnsureSuccessStatusCode();
        //    var responseString = await response.Content.ReadAsStringAsync();


        //    //Assert
        //    Assert.NotNull(responseString);

        //    //{"data":{"sites":[{"id":"5961f387-accd-49dc-b962-44029d0803ae","aliasId":"s1","siteName":"Sample Site","siteFolderName":null}]}}

        //    var jobj = JObject.Parse(responseString);
        //    Assert.NotNull(jobj);
        //    Assert.Equal("5961f387-accd-49dc-b962-44029d0803ae", jobj["data"]["sites"][0]["id"]);
        //   // Assert.Equal("ecb849ae-d1ee-4bb8-8f55-04be2e230441", jobj["data"]["sites"][0]["id"]);

        //}

        [Theory]
        [InlineData("/.well-known/openid-configuration")]
        public async Task T10000_CanReachDiscoveryEndpoint(string url)
        {
            // Arrange
            var client = GetUnauthenticatedClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/Identity")]
        public async Task T10020_GetShouldNotAllowAnonymousUser(string url)
        {
            //Arrange
            var client = GetUnauthenticatedClient();

            //Act
            var response = await client.GetAsync(url);

            //Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/Identity")]
        public async Task T10030_GetIdentityShouldReturnOkForAdmin(string url)
        {
            //Arrange
            var client = await GetAuthenticatedClient("admin@admin.com");

            //Act
            var response = await client.GetAsync(url);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }


        [Fact]
        public async Task T10040_AdminCanGetSiteList()
        {
            // Arrange
            var request = new GraphQLRequest
            {
                Query = @"
                    {
                      siteList {
                        id,
                        aliasId,
                        siteName,
                        siteFolderName
                      }
                    }"
            };

            var client = await GetAuthenticatedGraphQLClient();
            
            // Act
            var response = await client.SendQueryAsync(request);
            
            var result = response.GetDataFieldAs<List<SiteDescriptor>>("siteList");

            var id = result[0].Id.ToString();

            //Assert
            Assert.Equal("5961f387-accd-49dc-b962-44029d0803ae", id);
            
        }

        [Fact]
        public async Task T10050_AnonymousUserCannotGetSiteList()
        {
            // Arrange
            var request = new GraphQLRequest
            {
                Query = @"
                    {
                      siteList {
                        id,
                        aliasId,
                        siteName,
                        siteFolderName
                      }
                    }"
            };

            var client = GetUnauthenticatedGraphQLClient();

            // Act
            var response = await client.SendQueryAsync(request);
            
            //Assert
            Assert.Null(response.Data);

        }

        [Fact]
        public async Task T10060_ReturnOkForSiteQuery()
        {
            // Arrange
            var request = new GraphQLRequest
            {
                Query = @"
				query MyQuery($siteId: ID!){
					siteFromId (id: $siteId) {
                        id,
                        aliasId,
                        siteName,
                        privacyPolicy
                      }
				}",
                Variables = new
                {
                    siteId = "5961f387-accd-49dc-b962-44029d0803ae"
                }
            };
            
            var client = await GetAuthenticatedGraphQLClient();

            // Act
            var response = await client.SendQueryAsync(request);

            var result = response.GetDataFieldAs<SiteSettings>("siteFromId");

            Assert.NotNull(result);

            //Assert
            Assert.Equal("5961f387-accd-49dc-b962-44029d0803ae", result.Id.ToString());

        }

        [Fact]
        public async Task T10050_ReturnOkForSiteQueryAsDictionary()
        {
            // Arrange
            var request = new GraphQLRequest
            {
                Query = @"
				query MyQuery($siteId: ID!){
					siteFromId (id: $siteId) {
                        id,
                        aliasId,
                        siteName,
                        privacyPolicy
                      }
				}",
                Variables = new
                {
                    siteId = "5961f387-accd-49dc-b962-44029d0803ae"
                }
            };

            var client = await GetAuthenticatedGraphQLClient();

            // Act
            var response = await client.SendQueryAsync(request);

            var result = response.GetDataFieldAs<Dictionary<string,object>>("siteFromId");

            Assert.NotNull(result);

            //Assert
            Assert.Equal("5961f387-accd-49dc-b962-44029d0803ae", result["id"].ToString());

        }


    }
}
