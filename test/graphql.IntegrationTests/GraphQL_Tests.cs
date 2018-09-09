using graphql.WebApp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace graphql.IntegrationTests
{
    public class GraphQL_Tests : TestBase
    {
        public GraphQL_Tests(CustomWebApplicationFactory<Startup> factory):base(factory)
        {

        }

        [Fact]
        // [Trait("test", "integration")]
        public async Task T10010_ReturnOkForSitesQuery()
        {
            // Arrange
            const string query = @"{
                ""query"": ""query { sites { id, aliasId, siteName, siteFolderName } }""
            }";
            var content = new StringContent(query, Encoding.UTF8, "application/json");

            // Act
            var _client = GetUnauthenticatedClient();
            var response = await _client.PostAsync("/graphql", content);
            
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.NotNull(responseString);
            
            //{"data":{"sites":[{"id":"5961f387-accd-49dc-b962-44029d0803ae","aliasId":"s1","siteName":"Sample Site","siteFolderName":null}]}}

            var jobj = JObject.Parse(responseString);
            Assert.NotNull(jobj);
            Assert.Equal("5961f387-accd-49dc-b962-44029d0803ae", jobj["data"]["sites"][0]["id"]);
            //Assert.Equal("R2-D2", (string)jobj["data"]["hero"]["name"]);
        }


    }
}
