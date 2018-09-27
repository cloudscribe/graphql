using cloudscribe.Core.GraphQL.Services;
using cloudscribe.Core.Models;
using GraphQL.Authorization.AspNetCore;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace cloudscribe.Core.GraphQL.GraphTypes
{
    public class SiteType : ObjectGraphType<ISiteContext>
    {
        public SiteType(SiteService siteService)
        {

            FieldAsync<ListGraphType<SiteInfoType>>(
                "list",
                resolve: async context =>
                {
                    return await context.TryAsyncResolve(async c => await siteService.GetAllSites(c.CancellationToken));
                }
            )
            .AuthorizeWith("AdminPolicy")
            ;

            // public fields
            Field(x => x.Id, type: typeof(IdGraphType)).Description("The Uniqueidentifier of the site a guid");
            Field(x => x.AliasId).Description("A friendlier unique id");
            Field(x => x.SiteFolderName, nullable : true).Description("used for folder based multitenancy, this would correspond to the first folder segment in an url");
            Field(x => x.SiteName).Description("The name of the site");
            Field(x => x.PrivacyPolicy, nullable: true).Description("The privacy policy html.");
            Field(x => x.CreatedUtc,type: typeof(DateTimeGraphType)).Description("The creation date");
            Field(x => x.LastModifiedUtc, type: typeof(DateTimeGraphType)).Description("The last modified date");

            Field(x => x.TimeZoneId).Description("Default Time Zone of the site");
            Field(x => x.Theme, nullable: true).Description("The design theme of the site");
            Field(x => x.ForcedCulture, nullable: true).Description("The forced culture setting of the site, nullable");
            Field(x => x.ForcedUICulture, nullable: true).Description("The forced UI culture setting of the site, nullable");
            Field(x => x.GoogleAnalyticsProfileId, nullable: true).Description("The google analytics profile of the site, nullable");
            Field(x => x.AddThisDotComUsername, nullable: true).Description("The addthis.com profile of the site, nullable");

            Field(x => x.SiteIsClosed).Description("A boolean indicating if the site is closed to the public");
            Field(x => x.SiteIsClosedMessage, nullable: true).Description("The message to show to the public when the site is closed, nullable");

            Field(x => x.CompanyName, nullable: true).Description("The Company name");
            Field(x => x.CompanyStreetAddress, nullable: true).Description("The Company address line 1");
            Field(x => x.CompanyStreetAddress2, nullable: true).Description("The Company address line 1");
            Field(x => x.CompanyRegion, nullable: true).Description("The Company state or region");
            Field(x => x.CompanyLocality, nullable: true).Description("The Company city or locality");
            Field(x => x.CompanyCountry, nullable: true).Description("The Company country");
            Field(x => x.CompanyPostalCode, nullable: true).Description("The Company postal code");
            Field(x => x.CompanyPublicEmail, nullable: true).Description("The Company public email address");
            Field(x => x.CompanyPhone, nullable: true).Description("The Company phone number");
            Field(x => x.CompanyFax, nullable: true).Description("The Company FAX number");
            Field(x => x.CompanyWebsite, nullable: true).Description("The Company main website");

            //protected fields

            Field(x => x.RequireCookieConsent)
                .Description("A boolean indicating if cookie consent is required")
                .AuthorizeWith("AdminPolicy")
                ;

            Field(x => x.UseEmailForLogin)
                .Description("A boolean indicating if email is used for login vs a separate username")
                .AuthorizeWith("AdminPolicy")
                ;

            Field(x => x.PreferredHostName, nullable: true)
                .Description("used for host based multitenancy")
                .AuthorizeWith("AdminPolicy")
                ;

            Field(x => x.DefaultEmailFromAddress, nullable: true)
                .Description("The default email from address for the site")
                .AuthorizeWith("AdminPolicy")
                ;

            Field(x => x.DefaultEmailFromAlias, nullable: true)
                .Description("The default email from alias for the site")
                .AuthorizeWith("AdminPolicy")
                ;

            Field(x => x.AccountApprovalEmailCsv, nullable: true)
                .Description("A CSV listof email addresses to recieve new account notifications for approving new accounts")
                .AuthorizeWith("AdminPolicy")
                ;

            Field(x => x.AllowNewRegistration)
                .Description("A boolean indicating whether users are allowed to self register on the site")
                .AuthorizeWith("AdminPolicy")
                ;

            Field(x => x.AllowPersistentLogin)
                .Description("A boolean indicating whether to allow persistent login cookies")
                .AuthorizeWith("AdminPolicy")
                ;

            Field(x => x.CaptchaOnLogin)
                .Description("A boolean indicating whether to use a captcha on the login page")
                .AuthorizeWith("AdminPolicy")
                ;

            Field(x => x.CaptchaOnRegistration)
                .Description("A boolean indicating whether to use a captcha on the registration page")
                .AuthorizeWith("AdminPolicy")
                ;

            Field(x => x.DisableDbAuth)
                .Description("A boolean indicating whether to disable local authentication and only use social auth, setting this true is only allowed if social auth is configured.")
                .AuthorizeWith("AdminPolicy")
                ;

           


        }
    }
}
