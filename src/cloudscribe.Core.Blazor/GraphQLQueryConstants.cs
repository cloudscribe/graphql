namespace cloudscribe.Core.Blazor
{
    public static class GraphQLQueryConstants
    {
        #region Queries

        public const string SiteListQuery = @"
            {
              siteList {
                id,
                aliasId,
                siteName,
                siteFolderName
              }
            }";

        public const string SingleSiteCompanyInfoQuery = @"
                query MyQuery($siteId: ID!){
                companyInfo: siteFromId (id: $siteId) {
                id,
                companyName,
                companyCountry,
                companyFax,
                companyLocality,
                companyPhone,
                companyPostalCode,
                companyPublicEmail,
                companyWebsite,
                companyRegion,
                companyStreetAddress,
                companyStreetAddress2
                }
            }";

        #endregion

        #region Mutations

        public const string SiteCompanyInfoMutation = @"
            mutation updateSiteCompanyInfo($siteId: ID!, $update: CompanyInfoUpdateInput!){
                companyInfo: updateSiteCompanyInfo (id: $siteId, updateSiteCompanyInfo: $update) {
                id,
                companyName,
                companyCountry,
                companyFax,
                companyLocality,
                companyPhone,
                companyPostalCode,
                companyPublicEmail,
                companyWebsite,
                companyRegion,
                companyStreetAddress,
                companyStreetAddress2
                }
            }";

        #endregion



    }
}
