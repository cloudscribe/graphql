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

        public string Client_id { get; set; }

        public string Redirect_uri { get; set; }

        public string Response_type { get; set; }

        public string Scope { get; set; }

        public string Post_logout_redirect_uri { get; set; }

        public string Silent_redirect_uri { get; set; }

        public static async Task<Settings> GetSettings()
        {
            var settings = new Settings()
            {
                Authority = await AppSettings.Get("oidcAuthority"),
                Client_id = await AppSettings.Get("oidcClientId"),
                Redirect_uri = await AppSettings.Get("oidcRedirectUri"),
                Response_type = await AppSettings.Get("oidcResponseType"),
                Scope = await AppSettings.Get("oidcScopes"),
                Post_logout_redirect_uri = await AppSettings.Get("oidcPostLogoutRedirectUri"),
                Silent_redirect_uri = await AppSettings.Get("oidcSilentRedirectUri"),

            };

            return settings;
        }

    }
}
