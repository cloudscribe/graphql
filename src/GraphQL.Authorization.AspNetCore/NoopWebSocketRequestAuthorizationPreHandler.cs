using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace GraphQL.Authorization.AspNetCore
{
    public class NoopWebSocketRequestAuthorizationPreHandler : IWebSocketRequestAuthorizationPreHandler
    {
        public Task PrepareContext(HttpContext httpContext)
        {
            return Task.CompletedTask;
        }
    }
}
