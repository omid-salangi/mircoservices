namespace Basket.API.Common
{
    public class SiteSettings
    {
        public RedisSettings RedisSettings { get; set; }
    }

    public class RedisSettings
    {
        public string ConnectionString { get; set; }
    }
}
