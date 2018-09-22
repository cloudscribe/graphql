using System;
using System.Collections.Generic;
using System.Text;

namespace cloudscribe.Core.ApiModels
{
    public class CompanyInfoUpdateModel
    {
        //[StringLength(250, ErrorMessage = "Company name has a maximum length of 250 characters")]
        public string CompanyName { get; set; } 
        //[StringLength(250, ErrorMessage = "Address 1 has a maximum length of 250 characters")]
        public string CompanyStreetAddress { get; set; }
        //[StringLength(250, ErrorMessage = "Address 2 has a maximum length of 250 characters")]
        public string CompanyStreetAddress2 { get; set; } 
        //[StringLength(200, ErrorMessage = "City has a maximum length of 200 characters")]
        public string CompanyLocality { get; set; } 

        public string CompanyCountry { get; set; }

        //[StringLength(200, ErrorMessage = "State/Region has a maximum length of 200 characters")]
        public string CompanyRegion { get; set; } 
        //[StringLength(20, ErrorMessage = "Zip/Postal code has a maximum length of 20 characters")]
        public string CompanyPostalCode { get; set; }
        //[StringLength(20, ErrorMessage = "Phone has a maximum length of 20 characters")]
        public string CompanyPhone { get; set; }
        //[StringLength(20, ErrorMessage = "Fax has a maximum length of 20 characters")]
        public string CompanyFax { get; set; }

        //[EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
        //[StringLength(100, ErrorMessage = "Email has a maximum length of 100 characters")]
        public string CompanyPublicEmail { get; set; }

        //[Url(ErrorMessage = "The Company website field is not a valid url.")]
        //[StringLength(100, ErrorMessage = "Company website has a maximum length of 100 characters")]
        public string CompanyWebsite { get; set; }

    }
}
