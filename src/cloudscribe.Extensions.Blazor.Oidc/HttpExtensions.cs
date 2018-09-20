using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace cloudscribe.Extensions.Blazor.Oidc
{
    public static class HttpExtensions
    {
        public static async Task AddBeaerTokenIfAuthenticated(this HttpClient client, OidcService oidc)
        {
            if(oidc.CurrentUser == null)
            {
                await oidc.Init();
            }

            if(oidc.CurrentUser != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", oidc.CurrentUser.Access_Token);
            }
        }
    }
}
