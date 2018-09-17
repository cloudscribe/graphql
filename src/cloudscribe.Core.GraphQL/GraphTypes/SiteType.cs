using cloudscribe.Core.Models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace cloudscribe.Core.GraphQL.GraphTypes
{
    public class SiteType : ObjectGraphType<ISiteContext>
    {
        public SiteType()
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("The Uniqueidentifier of the site a guid");
            Field(x => x.AliasId).Description("A friendlier unique id");
            Field(x => x.SiteFolderName, nullable : true).Description("used for folder based multitenancy, this would correspond to the first folder segment in an url");
            Field(x => x.SiteName).Description("The name of the site");
            Field(x => x.PrivacyPolicy, nullable: true).Description("The privacy policy html.");
            Field(x => x.RequireCookieConsent).Description("A boolean indicating if cookie consent is required");
            Field(x => x.UseEmailForLogin).Description("A boolean indicating if email is used for login vs a separate username");

            Field(x => x.PreferredHostName, nullable: true).Description("used for host based multitenancy");

            Field(x => x.CreatedUtc,type: typeof(DateTimeGraphType)).Description("The creation date");
            Field(x => x.LastModifiedUtc, type: typeof(DateTimeGraphType)).Description("The last modified date");

        }
    }
}
