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
