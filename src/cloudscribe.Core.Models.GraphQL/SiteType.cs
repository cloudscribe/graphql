﻿using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace cloudscribe.Core.Models.GraphQL
{
    public class SiteType : ObjectGraphType<ISiteSettings>
    {
        public SiteType()
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("The Uniqueidentifier of the site a guid");
            Field(x => x.AliasId).Description("A friendlier unique id");
            Field(x => x.SiteFolderName, nullable : true).Description("used for folder based multitenancy, this would correspond to the first folder segment in an url");
            Field(x => x.SiteName).Description("The name of the site");

        }
    }
}