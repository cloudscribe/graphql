using cloudscribe.Core.Models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace cloudscribe.Core.GraphQL.GraphTypes
{
    public class SiteUserInfoType : ObjectGraphType<IUserInfo>
    {
        public SiteUserInfoType()
        {
            // public fields
            Field(x => x.Id, type: typeof(IdGraphType)).Description("The Uniqueidentifier of the user a guid");
            Field(x => x.Email).Description("The user's email address");
            Field(x => x.DisplayName).Description("The display name");

            Field(x => x.FirstName, nullable: true).Description("The First Name of the user if known");
            Field(x => x.LastName, nullable: true).Description("The Last Name of the user if known");

        }
    }
}
