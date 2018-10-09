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
            IPostQueriesSingleton postQueries,
            ILogger<PageService> logger
            )
        {
            _postQueries = postQueries;
            _log = logger;
        }

        private readonly IPostQueriesSingleton _postQueries;
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
           
            var posts = await _postQueries.GetPosts(projectId, category, includeUnpublished, pageNumber, pageSize, cancellationToken).ConfigureAwait(false);
     
            var result = new PagedResult<IPost>
            {
                TotalItems = posts.TotalItems,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = posts.Data
            };
            return result;

            
        }

        public async Task<IPost> GetPostBySlug(string projectId, string slug, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await _postQueries.GetPostBySlug(projectId, slug, cancellationToken).ConfigureAwait(false);
            return result.Post;
           
        }

    }
}
