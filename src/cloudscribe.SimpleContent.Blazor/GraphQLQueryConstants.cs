namespace cloudscribe.SimpleContent.Blazor
{
    public static class GraphQLQueryConstants
    {
        #region PageQueries

        public const string CmsPageQuery = @"
            query MyQuery($projectId: String!, $slug: String!){
                getPageFromSlug (projectId: $projectId, slug: $slug) {
                    id,
                    projectId,
                    title,
                    content,
                    metaDescription,
                    author,
                    pubDate
                  }
            }";

        #endregion

        #region BlogQueries

        public const string CmsPostListQuery = @"
            query PostListQuery($projectId: String!, $category: String, $pageNumber : Int!, $pageSize : Int!){
                postList (projectId: $projectId, category: $category, pageNumber: $pageNumber, pageSize: $pageSize) {
                    pageNumber,
                    pageSize,
                    totalItems,
                    data:posts {
                      id,
                      title,
                      contentType,
                      metaDescription,
                      author,
                      pubDate,
                      content
                      }
                }
            }";

        #endregion


    }
}
