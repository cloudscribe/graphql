using System.Security.Claims;

namespace GraphQL.Authorization.AspNetCore
{
    public class GraphQLUserContext : IProvideClaimsPrincipal
    {
        public ClaimsPrincipal User { get; set; }
    }
}
