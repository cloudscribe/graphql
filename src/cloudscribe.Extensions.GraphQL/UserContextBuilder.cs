using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace cloudscribe.Extensions.GraphQL
{
    public static class UserContextBuilder
    {
        public static async Task<GraphQLUserContext> BuildUserContext(HttpContext context)
        {
            var userContext = new GraphQLUserContext();
            userContext.User = context.User;
            var result = await context.AuthenticateAsync("Bearer");
            if(result.Succeeded)
            {
                userContext.User = result.Principal;
            }
            return userContext;

        }
    }
}
