using GraphQL.Common.Request;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace cloudscribe.Extensions.Blazor.GraphQL
{
    public static class HttpExtensions
    {
        public static async Task<GraphQLResult<T>> SendQuery<T>(this HttpClient client, string endPoint, GraphQLRequest query) where T : class
        {
            var queryJson = Json.Serialize(query);
            var response = await client.PostAsync(endPoint, new StringContent(queryJson, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            ErrorModel errorModel = null;
            T success = null;
            if (result.Contains("errors"))
            {
                errorModel = Json.Deserialize<ErrorModel>(result);
            }
            else
            {
                success = Json.Deserialize<T>(result);
            }

            return new GraphQLResult<T>(success, errorModel);

        }
    }
}
