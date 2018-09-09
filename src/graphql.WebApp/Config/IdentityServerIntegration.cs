using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IdentityServerIntegration
    {
        public static IServiceCollection SetupIdentityServerIntegrationAndCORSPolicy(
            this IServiceCollection services,
            IConfiguration config,
            IHostingEnvironment environment,
            ILogger log,
            bool sslIsAvailable,
            bool disableIdentityServer,
            out bool didSetupIdServer
            )
        {
            didSetupIdServer = false;

            if (!disableIdentityServer)
            {
                try
                {
                    var idsBuilder = services.AddIdentityServerConfiguredForCloudscribe(options =>
                    {
                        options.UserInteraction.ErrorUrl = "/oops/error";

                    })

                        .AddCloudscribeCoreNoDbIdentityServerStorage()
                        .AddCloudscribeIdentityServerIntegrationMvc();
                    if (environment.IsProduction())
                    {
                        // *** IMPORTANT CONFIGURATION NEEDED HERE *** 
                        // can't use .AddDeveloperSigningCredential in production it will throw an error
                        // https://identityserver4.readthedocs.io/en/dev/topics/crypto.html
                        // https://identityserver4.readthedocs.io/en/dev/topics/startup.html#refstartupkeymaterial
                        // you need to create an X.509 certificate (can be self signed)
                        // on your server and configure the cert file path and password name in appsettings.json
                        // OR change this code to wire up a certificate differently
                        log.LogWarning("setting up identityserver4 for production");
                        var certPath = config.GetValue<string>("AppSettings:IdServerSigningCertPath");
                        var certPwd = config.GetValue<string>("AppSettings:IdServerSigningCertPassword");
                        if (!string.IsNullOrWhiteSpace(certPath) && !string.IsNullOrWhiteSpace(certPwd))
                        {
                            var cert = new X509Certificate2(
                            File.ReadAllBytes(certPath),
                            certPwd,
                            X509KeyStorageFlags.MachineKeySet |
                            X509KeyStorageFlags.PersistKeySet |
                            X509KeyStorageFlags.Exportable);

                            idsBuilder.AddSigningCredential(cert);
                            didSetupIdServer = true;
                        }
                    }
                    else
                    {
                        idsBuilder.AddDeveloperSigningCredential(); // don't use this for production
                        didSetupIdServer = true;
                    }

                    services.AddCors(options =>
                    {
                        // this defines a CORS policy called "default"
                        // add your IdentityServer client apps and apis to allow access to them
                        options.AddPolicy("default", policy =>
                        {
                            policy.WithOrigins("http://localhost:63720", "http://localhost:63720")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                        });
                    });

                }
                catch (Exception ex)
                {
                    log.LogError($"failed to setup identityserver4 {ex.Message} {ex.StackTrace}");
                }

            }


            return services;
        }

    }
}
