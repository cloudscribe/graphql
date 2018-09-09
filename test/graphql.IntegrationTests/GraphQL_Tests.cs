using cloudscribe.Core.Models;
using graphql.WebApp;
using GraphQL.Common.Request;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

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
        //        ""query"": ""query { sites { id, aliasId, siteName, siteFolderName } }""
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
            
        //}

        [Fact]
        public async Task T10010_ReturnOkForSitesQuery()
        {
            // Arrange
            var request = new GraphQLRequest
            {
                Query = @"
                    {
                      sites {
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
            
            var result = response.GetDataFieldAs<List<SiteInfo>>("sites");

            var id = result[0].Id.ToString();

            //Assert
            Assert.Equal("5961f387-accd-49dc-b962-44029d0803ae", id);
            
        }

       
    }
}
