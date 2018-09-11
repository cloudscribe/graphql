using cloudscribe.Core.GraphQL.Models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

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
