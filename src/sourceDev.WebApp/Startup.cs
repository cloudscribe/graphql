using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;


namespace sourceDev.WebApp
{
    public class Startup
    {
        public Startup(
            IConfiguration configuration,
            IHostingEnvironment env,
            ILogger<Startup> logger
            )
        {
            _configuration = configuration;
            _environment = env;
            _log = logger;

            _sslIsAvailable = _configuration.GetValue<bool>("AppSettings:UseSsl");
            _disableIdentityServer = _configuration.GetValue<bool>("AppSettings:DisableIdentityServer");
        }

        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _environment;
        private readonly bool _sslIsAvailable;
        private readonly bool _disableIdentityServer;
        private bool _didSetupIdServer = false;
        private readonly ILogger _log;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //// **** VERY IMPORTANT *****
            // This is a custom extension method in Config/DataProtection.cs
            // These settings require your review to correctly configur data protection for your environment
            services.SetupDataProtection(_configuration, _environment);

            services.AddAuthorization(options =>
            {
                //https://docs.asp.net/en/latest/security/authorization/policies.html
                //** IMPORTANT ***
                //This is a custom extension method in Config/Authorization.cs
                //That is where you can review or customize or add additional authorization policies
                options.SetupAuthorizationPolicies();

            });

            //// **** IMPORTANT *****
            // This is a custom extension method in Config/CloudscribeFeatures.cs
            services.SetupDataStorage(_configuration);

            //*** Important ***
            // This is a custom extension method in Config/IdentityServerIntegration.cs
            // You should review this and understand what it does before deploying to production
            services.SetupIdentityServerIntegrationAndCORSPolicy(
                _configuration,
                _environment,
                _log,
                _sslIsAvailable,
                _disableIdentityServer,
                out _didSetupIdServer
                );

            //*** Important ***
            // This is a custom extension method in Config/CloudscribeFeatures.cs
            services.SetupCloudscribeFeatures(_configuration);

            //*** Important ***
            // This is a custom extension method in Config/Localization.cs
            services.SetupLocalization();
            
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = cloudscribe.Core.Identity.SiteCookieConsent.NeedsConsent;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
                options.ConsentCookie.Name = "cookieconsent_status";
            });

            services.Configure<Microsoft.AspNetCore.Mvc.CookieTempDataProviderOptions>(options =>
            {
                options.Cookie.IsEssential = true;
            });

            services.AddGraphQLServices(_environment);

            //*** Important ***
            // This is a custom extension method in Config/RoutingAndMvc.cs
            services.SetupMvc(_sslIsAvailable);

            // This is a custom extension method in Config/IdentityServerIntegration.cs
            // the integration tests set this up different so this is false in appsetings.Test.json
            var setupApiAuthentication = _configuration.GetValue<bool>("AppSettings:SetupApiAuthentication");
            if (setupApiAuthentication)
            {
                services.SetupIdentityServerApiAuthentication();
            }
            // the integration tests only use Bearer auth so need to make it default for testing
            var forceBearerAuth = _configuration.GetValue<bool>("AppSettings:ForceBearerAuth");

            if(forceBearerAuth)
            {
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "Bearer";
                    //options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                    //options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                });
            }


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IOptions<cloudscribe.Core.Models.MultiTenantOptions> multiTenantOptionsAccessor,
            IOptions<RequestLocalizationOptions> localizationOptionsAccessor
            )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/oops/error");
                if (_sslIsAvailable)
                {
                    app.UseHsts();
                }
            }
            if (_sslIsAvailable)
            {
                app.UseHttpsRedirection();
            }
            app.UseStaticFiles();
            app.UseCloudscribeCommonStaticFiles();
            app.UseCookiePolicy();

            app.UseRequestLocalization(localizationOptionsAccessor.Value);
            app.UseCors("default"); //use Cors with policy named default, defined above

            var multiTenantOptions = multiTenantOptionsAccessor.Value;

            app.UseCloudscribeCore(
                    loggerFactory,
                    multiTenantOptions,
                    _sslIsAvailable);

            if (!_disableIdentityServer && _didSetupIdServer)
            {
                try
                {
                    app.UseIdentityServer();
                }
                catch (Exception ex)
                {
                    _log.LogError($"failed to setup identityserver4 {ex.Message} {ex.StackTrace}");
                }
            }

            // add http for Schema at default url /graphql
            app.UseGraphQL<ISchema>("/graphql");

            // use graphql-playground at default url /ui/playground
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions
            {
                Path = "/ui/playground"
            });

            app.UseMvc(routes =>
            {
                var useFolders = multiTenantOptions.Mode == cloudscribe.Core.Models.MultiTenantMode.FolderName;
                //*** IMPORTANT ***
                // this is in Config/RoutingAndMvc.cs
                // you can change or add routes there
                routes.UseCustomRoutes(useFolders);
            });

        }



    }
}