using cloudscribe.Extensions.GraphQL;
using GraphQL.Types;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddGraphQLCompositeSchemaAndQuery(this IServiceCollection services)
        {
            services.AddSingleton<CompositeQuery>();
            services.AddSingleton<CompositeMutation>();
            services.AddSingleton<CompositeSubscription>();
            services.AddSingleton<ISchema, CompositeSchema>();


            return services;
        }
    }
}
