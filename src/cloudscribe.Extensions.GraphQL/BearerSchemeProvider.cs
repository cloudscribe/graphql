//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Options;
//using Microsoft.Extensions.Primitives;

//namespace cloudscribe.Extensions.GraphQL
//{
//    public class BearerSchemeProvider : AuthenticationSchemeProvider
//    {
//        public BearerSchemeProvider(
//            IOptions<AuthenticationOptions> options,
//            IHttpContextAccessor httpContextAccessor
//            ):base(options)
//        {
//            _httpContextAccessor = httpContextAccessor;
//        }

//        protected BearerSchemeProvider(
//            IOptions<AuthenticationOptions> options, 
//            IDictionary<string, AuthenticationScheme> schemes,
//            IHttpContextAccessor httpContextAccessor
//            ) :base(options, schemes)
//        {
//            _httpContextAccessor = httpContextAccessor;
//        }

//        private readonly IHttpContextAccessor _httpContextAccessor;

//        public override async Task<AuthenticationScheme> GetDefaultAuthenticateSchemeAsync()
//        {
//            if(_httpContextAccessor.HttpContext != null)
//            {
//                var context = _httpContextAccessor.HttpContext;
//                if (context.Request.Path.StartsWithSegments("/graphql"))
//                {
//                    if (context.Request.Headers.TryGetValue("Authorization", out StringValues authToken))
//                    {
//                        string authHeader = authToken.First();
//                        if(authHeader.StartsWith("Bearer"))
//                        {
//                            var scheme = await base.GetSchemeAsync("Bearer");
//                            return scheme;
//                        }
                        
//                    }
//                }
//            }

//            return await base.GetDefaultAuthenticateSchemeAsync();
//        }
//    }
//}
