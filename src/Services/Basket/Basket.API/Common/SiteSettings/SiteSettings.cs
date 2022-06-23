namespace Basket.API.Common
{
    public class SiteSettings
    {
        public RedisSettings RedisSettings { get; set; }
        public GrpcSettings GrpcSettings { get; set; }
    }

    public class RedisSettings
    {
        public string ConnectionString { get; set; }
    }

    public class GrpcSettings
    {
        public string DiscountUrl { get; set; }
    }
}
