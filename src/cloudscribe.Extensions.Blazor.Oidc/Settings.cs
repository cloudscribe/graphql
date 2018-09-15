using System.Threading.Tasks;
using cloudscribe.Extensions.Blazor.Config;

namespace cloudscribe.Extensions.Blazor.Oidc
{
    public class Settings
    {
        private Settings()
        {

        }

        public string Authority { get; set; }

        public string ClientId { get; set; }

        public string RedirectUri { get; set; }

        public string ResponseType { get; set; }

        public string Scopes { get; set; }

        public string PostLogoutRedirectUri { get; set; }

        public static async Task<Settings> GetSettings()
        {
            var settings = new Settings()
            {
                Authority = await AppSettings.Get("oidcAuthority"),
                ClientId = await AppSettings.Get("oidcClientId"),
                RedirectUri = await AppSettings.Get("oidcRedirectUrl"),
                ResponseType = await AppSettings.Get("oidcResponseType"),
                Scopes = await AppSettings.Get("oidcScopes"),
                PostLogoutRedirectUri = await AppSettings.Get("oidcPostLogoutRedirectUri")

            };

            return settings;
        }

    }
}
