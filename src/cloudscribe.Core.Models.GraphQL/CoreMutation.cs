using cloudscribe.Core.Services.GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace cloudscribe.Core.Models.GraphQL
{
    public class CoreMutation : ObjectGraphType
    {
        public CoreMutation(
            SiteService siteService
            )
        {
            Name = "CoreMutation";
        }
    }
}
