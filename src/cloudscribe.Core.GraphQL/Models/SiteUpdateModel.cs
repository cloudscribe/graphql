using System;
using System.Collections.Generic;
using System.Text;

namespace cloudscribe.Core.GraphQL.Models
{
    public class SiteUpdateModel
    {
        public string SiteName { get; set; }
        public bool? AllowPersistentLogin { get; set; }
    }
}
