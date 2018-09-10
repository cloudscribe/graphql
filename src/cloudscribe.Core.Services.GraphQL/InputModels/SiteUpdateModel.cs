using System;
using System.Collections.Generic;
using System.Text;

namespace cloudscribe.Core.Services.GraphQL.InputModels
{
    public class SiteUpdateModel
    {
        public string SiteName { get; set; }
        public bool? AllowPersistentLogin { get; set; }
    }
}
