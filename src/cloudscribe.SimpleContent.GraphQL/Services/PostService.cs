using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace cloudscribe.SimpleContent.GraphQL.Services
{
    public class PostService
    {
        public PostService(
            IServiceProvider serviceProvider,
            ILogger<PageService> logger
            )
        {
            _serviceProvider = serviceProvider;
            _log = logger;
        }

        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _log;

    }
}
