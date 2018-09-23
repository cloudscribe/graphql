using cloudscribe.Extensions.Blazor.Oidc;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace sourceDev.BlazorApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<OidcService>();

            services.AddToaster(config =>
            {
                config.PositionClass = Sotsera.Blazor.Toaster.Core.Models.Defaults.Classes.Position.BottomFullWidth;
                config.PreventDuplicates = true;
                config.NewestOnTop = false;
                config.VisibleStateDuration = 3500;
            });
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("body");
        }
    }
}
