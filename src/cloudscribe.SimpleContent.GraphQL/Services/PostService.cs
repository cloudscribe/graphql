using cloudscribe.Pagination.Models;
using cloudscribe.SimpleContent.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

        public async Task<PagedResult<IPost>> GetPosts(
            string projectId,
            string category,
            int pageNumber,
            int pageSize,
            bool includeUnpublished,
            CancellationToken cancellationToken = default(CancellationToken)
            )
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var queries = scopedServices.GetService<IPostQueries>();
                var posts = await queries.GetPosts(projectId, category, includeUnpublished, pageNumber, pageSize, cancellationToken).ConfigureAwait(false);
     
                var result = new PagedResult<IPost>
                {
                    TotalItems = posts.TotalItems,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    Data = posts.Data
                };
                return result;

            }
        }

        public async Task<IPost> GetPostBySlug(string projectId, string slug, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var queries = scopedServices.GetService<IPostQueries>();
                var result = await queries.GetPostBySlug(projectId, slug, cancellationToken).ConfigureAwait(false);
                return result.Post;

            }
        }

    }
}
