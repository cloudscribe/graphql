using System.Security.Claims;

namespace cloudscribe.Extensions.GraphQL
{
    public class GraphQLUserContext
    {
        public ClaimsPrincipal User { get; set; }
    }
}
