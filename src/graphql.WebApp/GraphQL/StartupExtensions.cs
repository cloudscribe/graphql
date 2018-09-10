using graphql.WebApp.GraphQL;
using GraphQL;
using GraphQL.Http;
using GraphQL.Server;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddGraphQLServices(this IServiceCollection services)
        {
            services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));

            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();

            services.AddServicesForCloudscribeCore();
            services.AddGraphQLModelsForCloudscribeCore();


            //services.AddSingleton<RootQueryType>();
            //services.AddSingleton<ISchema, RootSchema>();
            services.AddGraphQLCompositeSchemaAndQuery();

            services.AddGraphQL(_ =>
            {
                _.EnableMetrics = true;
                _.ExposeExceptions = true;
            })
            .AddUserContextBuilder(httpContext => new graphql.WebApp.GraphQL.GraphQLUserContext { User = httpContext.User });


            return services;
        }
    }
}
