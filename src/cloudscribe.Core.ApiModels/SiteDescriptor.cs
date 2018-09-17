using System;

namespace cloudscribe.Core.ApiModels
{
    public class SiteDescriptor
    {
        public Guid Id { get; set; }

        public string AliasId { get; set; }

        public string SiteName { get; set; }

        public string SiteFolderName { get; set; }

        public string PreferredHostName { get; set; }

        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
        public DateTime LastModifiedUtc { get; set; } = DateTime.UtcNow;
    }
}
