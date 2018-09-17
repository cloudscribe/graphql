using System;
using System.Collections.Generic;
using System.Text;

namespace cloudscribe.Core.ApiModels
{
    public class SiteUpdateModel
    {
        public string SiteName { get; set; }
        public bool? AllowPersistentLogin { get; set; }
    }
}
