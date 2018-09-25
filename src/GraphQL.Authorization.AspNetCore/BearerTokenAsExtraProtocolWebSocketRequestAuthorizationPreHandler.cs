using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace GraphQL.Authorization.AspNetCore
{
    /// <summary>
    /// extracts a bearer token passed as a second Sec-WebSocket-Protocol paramter and adds an Authorization header to the request.
    /// </summary>
    public class BearerTokenAsExtraProtocolWebSocketRequestAuthorizationPreHandler : IWebSocketRequestAuthorizationPreHandler
    {
        public Task PrepareContext(HttpContext httpContext)
        {
            var header = httpContext.Request.Headers["Sec-WebSocket-Protocol"].ToArray();
            if (header != null && header.Length == 1)
            {
                var headerval = header[0].Split(',');
                if (headerval.Length == 2)
                {
                    var bearerToken = headerval[1].Trim();
                    httpContext.Request.Headers.Add("Authorization", "Bearer " + bearerToken);
                }
            }

            return Task.CompletedTask;
        }
    }
}
