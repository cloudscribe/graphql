using GraphQL.Server.Transports.Subscriptions.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace GraphQL.Authorization.AspNetCore
{
    public class AddUserContextMessageListener : IOperationMessageListener
    {
        public AddUserContextMessageListener(
            IHttpContextAccessor httpContextAccessor,
            IWebSocketRequestAuthorizationPreHandler webSocketPreHandler
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _webSocketPreHandler = webSocketPreHandler;
        }

        private IHttpContextAccessor _httpContextAccessor;
        private readonly IWebSocketRequestAuthorizationPreHandler _webSocketPreHandler;

        public async Task BeforeHandleAsync(MessageHandlingContext context)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if(httpContext != null)
            {
                if(httpContext.WebSockets.IsWebSocketRequest && (!httpContext.User.Identity.IsAuthenticated))
                {
                    await _webSocketPreHandler.PrepareContext(httpContext);
                }

                var userContext = new GraphQLUserContext
                {
                    User = httpContext.User
                };
                // if the default auth scheme is cookies
                // users from remote clients that pass bearer token are not automatically authenticated
                if (!httpContext.User.Identity.IsAuthenticated)
                {
                    var result = await httpContext.AuthenticateAsync("Bearer");
                    if (result.Succeeded)
                    {
                        userContext.User = result.Principal;
                    }
                }
                context.Properties.TryAdd("UserContext", userContext);
            }   
        }

        public Task HandleAsync(MessageHandlingContext context)
        {
            return Task.CompletedTask;
        }

        public Task AfterHandleAsync(MessageHandlingContext context)
        {
            return Task.CompletedTask;
        }
    }
}
