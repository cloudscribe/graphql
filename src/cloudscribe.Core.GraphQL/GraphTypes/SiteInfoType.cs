using cloudscribe.Core.Models;
using GraphQL.Types;

namespace cloudscribe.Core.GraphQL.GraphTypes
{
    public class SiteInfoType : ObjectGraphType<ISiteInfo>
    {
        public SiteInfoType()
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("The Uniqueidentifier of the site a guid");
            Field(x => x.AliasId).Description("A friendlier unique id");
            Field(x => x.SiteFolderName, nullable: true).Description("used for folder based multitenancy, this would correspond to the first folder segment in an url");
            Field(x => x.SiteName).Description("The name of the site");
            Field(x => x.PreferredHostName, nullable: true).Description("used for host based multitenancy");

            

        }
    }
}
