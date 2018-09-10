

query{
  sites {
    id,
    aliasId,
    siteName,
    siteFolderName
  }
}

query{
  sites {
    id,
    aliasId,
    siteName,
    siteFolderName
  },
  site (id: "5961f387-accd-49dc-b962-44029d0803ae"){
    id,
    siteName,
    privacyPolicy
  }
}
