using GraphQL;
using GraphQL.Http;
using GraphQL.Server;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddGraphQLServices(this IServiceCollection services)
        {
            services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));

            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();

            services.AddGraphQLForCloudscribeCore();
            services.AddGraphQLCompositeSchemaAndQuery();

            services.AddGraphQL(_ =>
            {
                _.EnableMetrics = true;
                _.ExposeExceptions = true;
            })
            .AddUserContextBuilder(httpContext => new cloudscribe.Extensions.GraphQL.GraphQLUserContext { User = httpContext.User });


            return services;
        }
    }
}
