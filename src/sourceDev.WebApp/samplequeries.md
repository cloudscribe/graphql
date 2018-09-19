# Sample Queries


    query{
      siteList {
        id,
        aliasId,
        siteName,
        siteFolderName
      }
    }

    query{
      siteFromId (id: "5961f387-accd-49dc-b962-44029d0803ae"){
        id,
        siteName,
        privacyPolicy
      }
    }


    mutation{
        updateSite(id:"5961f387-accd-49dc-b962-44029d0803ae", 
        updateSite:( )) {
        id,
        aliasId,
        siteName,
        siteFolderName
        }
    }

	
	query PostListQuery($projectId: String!, $category: String, $pageNumber : Int!, $pageSize : Int!){
					postList (projectId: $projectId, category: $category, pageNumber: $pageNumber, pageSize: $pageSize) {
                        pageNumber,
                        pageSize,
                        totalItems,
                        posts {
                          id,
                          title,
                          content,
                          contentType,
                          metaDescription,
                          author,
                          pubDate
                        }
                      }
				}
				
				{
  "projectId" : "5961f387-accd-49dc-b962-44029d0803ae",
  "category": "",
  "pageNumber": 1,
  "pageSize": 10
}


query PageQuery($projectId: String!, $slug: String!){
					getPageFromSlug (projectId: $projectId, slug: $slug) {
                        id,
                        projectId,
                        title,
                        content,
                        metaDescription,
                        author,
                        pubDate
                      }
				}

{
  "projectId" : "5961f387-accd-49dc-b962-44029d0803ae",
  "slug": "home"
}



query myTwoQueries($siteId : ID!) {
   mysites:siteList {
        id,
        aliasId,
        siteName,
        siteFolderName
  },
      siteFromId (id: $siteId){
        id,
        siteName,
        privacyPolicy
      }
    }
	
	{
  "siteId" : "5961f387-accd-49dc-b962-44029d0803ae"
}