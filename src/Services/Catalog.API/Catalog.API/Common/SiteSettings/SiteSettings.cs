namespace Catalog.API.Common.SiteSettings
{
    public class SiteSettings
    {
        public MongoDataBaseSetting MongoDataBaseSetting { get; set; }

    }

    public class MongoDataBaseSetting
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
