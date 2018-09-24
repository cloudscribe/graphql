using System;
using System.Collections.Generic;
using System.Text;

namespace cloudscribe.Core.ApiModels
{
    public class SiteDetails : SiteDescriptor
    {
        public bool RequireCookieConsent { get; set; } = true;
        public string CookiePolicySummary { get; set; }

        public string PrivacyPolicy { get; set; }

        public bool AllowNewRegistration { get; set; } = true;
        public bool AllowPersistentLogin { get; set; } = true;
        public bool RequireConfirmedEmail { get; set; } = false;

        public string RecaptchaPrivateKey { get; set; }

        public string RecaptchaPublicKey { get; set; }

        public bool UseInvisibleRecaptcha { get; set; } = false;

        public string AddThisDotComUsername { get; set; }

        

        public string CompanyName { get; set; }


        public string CompanyStreetAddress { get; set; }

        public string CompanyStreetAddress2 { get; set; }

        public string CompanyLocality { get; set; }

        public string CompanyRegion { get; set; }

        public string CompanyPostalCode { get; set; }

        public string CompanyCountry { get; set; }

        public string CompanyPhone { get; set; }

        public string CompanyFax { get; set; }

        public string CompanyPublicEmail { get; set; }

        public string CompanyWebsite { get; set; }


        public string DefaultEmailFromAddress { get; set; }

        public string DefaultEmailFromAlias { get; set; }

    }
}
