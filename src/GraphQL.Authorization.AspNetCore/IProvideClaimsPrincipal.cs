using System.Security.Claims;

namespace GraphQL.Authorization.AspNetCore
{
    public interface IProvideClaimsPrincipal
    {
        ClaimsPrincipal User { get; }
    }
}
