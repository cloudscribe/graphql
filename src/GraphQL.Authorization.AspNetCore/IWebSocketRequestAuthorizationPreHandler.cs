using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace GraphQL.Authorization.AspNetCore
{
    public interface IWebSocketRequestAuthorizationPreHandler
    {
        Task PrepareContext(HttpContext httpContext);
    }
}
