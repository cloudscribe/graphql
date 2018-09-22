using cloudscribe.Core.ApiModels;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace cloudscribe.Core.GraphQL.GraphTypes
{
    public class CompanyInfoUpdateType : InputObjectGraphType<CompanyInfoUpdateModel>
    {
        public CompanyInfoUpdateType()
        {
            Name = "CompanyInfoUpdateInput";

            Field(x => x.CompanyCountry, nullable: true).Description("");
            Field(x => x.CompanyFax, nullable: true).Description("");
            Field(x => x.CompanyLocality, nullable: true).Description("");
            Field(x => x.CompanyName, nullable: true).Description("");
            Field(x => x.CompanyPhone, nullable: true).Description("");
            Field(x => x.CompanyPostalCode, nullable: true).Description("");
            Field(x => x.CompanyPublicEmail, nullable: true).Description("");
            Field(x => x.CompanyWebsite, nullable: true).Description("");
            Field(x => x.CompanyRegion, nullable: true).Description("");
            Field(x => x.CompanyStreetAddress, nullable: true).Description("");
            Field(x => x.CompanyStreetAddress2, nullable: true).Description("");
            
        }

    }
}
