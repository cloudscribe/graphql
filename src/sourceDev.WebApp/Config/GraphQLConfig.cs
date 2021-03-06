﻿using GraphQL;
using GraphQL.Authorization.AspNetCore;
using GraphQL.Http;
using GraphQL.Server;
using Microsoft.AspNetCore.Hosting;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class GraphQLConfig
    {
        public static IServiceCollection AddGraphQLServices(
            this IServiceCollection services,
            IHostingEnvironment hostingEnvironment
            )
        {
            services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();
            
            services.AddGraphQLForCloudscribeCore();
            services.AddGraphQLForCloudscribeSimpleContent();
            services.AddGraphQLCompositeSchemaAndQuery();

            services.AddTransient<IWebSocketRequestAuthorizationPreHandler, BearerTokenAsExtraProtocolWebSocketRequestAuthorizationPreHandler>();

            services.AddGraphQLAuthorization(hostingEnvironment.IsDevelopment());
            services.AddGraphQL(_ =>
            {
                _.EnableMetrics = true;
                _.ExposeExceptions = true;
               
            })
            .AddUserContextBuilder(UserContextBuilder.BuildUserContext)
            .AddWebSockets()
            ;


            return services;
        }

    }
}
