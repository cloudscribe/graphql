using GraphQL.Server.Transports.Subscriptions.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Authorization.AspNetCore
{
    public class AddUserContextMessageListener : IOperationMessageListener
    {
        public AddUserContextMessageListener(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private IHttpContextAccessor _httpContextAccessor;

        public async Task BeforeHandleAsync(MessageHandlingContext context)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if(httpContext != null)
            {
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
                //context.Properties.AddOrUpdate<string, object>("UserContext", userContext);
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
