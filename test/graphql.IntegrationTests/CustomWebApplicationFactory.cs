﻿using graphql.IntegrationTests.Helpers;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using sourceDev.WebApp;

namespace graphql.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
    {
        private TestHttpMessageHandler _testMessageHandler = new TestHttpMessageHandler();

        protected override TestServer CreateServer(IWebHostBuilder builder)
        {
            var server = base.CreateServer(builder);

            var innerHttpMessageHandler = server.CreateHandler();
            _testMessageHandler.WrappedMessageHandler = innerHttpMessageHandler;

            return server;
        }

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            var builder = base.CreateWebHostBuilder();
            return builder;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config =>
            {
                //this is configured to use the in memory db
                config.AddJsonFile("appsettings.Test.json", false);

            });

            // this is so that when idserver token validation makes http calls it uses a client created for the test host
            //https://github.com/aspnet/Hosting/issues/927
            builder.ConfigureServices(services =>
            {
                services.AddAuthentication()
                .AddIdentityServerAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = "http://localhost";
                    options.ApiName = "api";
                    options.ApiSecret = "secret";
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.IntrospectionBackChannelHandler = _testMessageHandler;
                    options.IntrospectionDiscoveryHandler = _testMessageHandler;
                    options.JwtBackChannelHandler = _testMessageHandler;

                    options.ForwardDefaultSelector = ctx =>
                     {
                         if (ctx.Request.Path.StartsWithSegments("/api") || ctx.Request.Path.StartsWithSegments("/graphql"))
                         {
                             return IdentityServerAuthenticationDefaults.AuthenticationScheme;
                         }
                         else
                         {
                             return "Identity.Application";
                         }
                     };

                });

            });

        }
    }
}
