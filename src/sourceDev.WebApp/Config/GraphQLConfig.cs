using cloudscribe.Extensions.GraphQL;
using GraphQL;
using GraphQL.Authorization.AspNetCore;
using GraphQL.Http;
using GraphQL.Server;
using GraphQL.Validation;
using Microsoft.AspNetCore.Authentication;
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

            //services.AddSingleton<IAuthenticationSchemeProvider, BearerSchemeProvider>();
            if(hostingEnvironment.IsDevelopment())
            {
                services.Configure<AuthorizationValidationOptions>(options =>
                {
                    options.IncludeErrorDetails = true;
                });
            }

            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();

            services.AddTransient<IValidationRule, AuthorizationValidationRule>();

            services.AddGraphQLForCloudscribeCore();
            services.AddGraphQLCompositeSchemaAndQuery();

            services.AddGraphQL(_ =>
            {
                _.EnableMetrics = true;
                _.ExposeExceptions = true;
               
            })
            .AddUserContextBuilder(UserContextBuilder.BuildUserContext)
            ;


            return services;
        }

    }
}
