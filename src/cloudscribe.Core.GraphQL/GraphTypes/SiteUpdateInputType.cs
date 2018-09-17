using cloudscribe.Core.ApiModels;
using GraphQL.Types;

namespace cloudscribe.Core.GraphQL.GraphTypes
{
    public class SiteUpdateInputType : InputObjectGraphType<SiteUpdateModel>
    {
        public SiteUpdateInputType()
        {
            Name = "SiteUpdateInput";

            Field(x => x.SiteName, nullable: true);
            Field(x => x.AllowPersistentLogin, nullable: true);


        }
    }
}
