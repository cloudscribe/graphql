using GraphQL.Authorization.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace GraphQL.Authorization.AspNetCore
{
    public static class UserContextBuilder
    {
        public static async Task<GraphQLUserContext> BuildUserContext(HttpContext context)
        {
            var userContext = new GraphQLUserContext
            {
                User = context.User
            };
            // if the default auth scheme is cookies
            // users from remote clients that pass bearer token are not automatically authenticated

            if (!context.User.Identity.IsAuthenticated)
            {
                var result = await context.AuthenticateAsync("Bearer");
                if (result.Succeeded)
                {
                    userContext.User = result.Principal;
                }
            }

            return userContext;

        }
    }
}
