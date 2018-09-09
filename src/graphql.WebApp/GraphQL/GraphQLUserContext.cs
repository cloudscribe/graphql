using System.Security.Claims;

namespace graphql.WebApp.GraphQL
{
    public class GraphQLUserContext
    {
        public ClaimsPrincipal User { get; set; }
    }
}
