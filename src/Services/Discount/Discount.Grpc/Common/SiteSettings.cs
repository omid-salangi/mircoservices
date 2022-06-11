namespace Discount.Grpc.Common
{
    public class SiteSettings
    {
        public PostgresDbSettings PostgresDbSettings { get; set; }
    }
    public class PostgresDbSettings
    {
      public string PostgresDbConnection { get; set; }
    }
}
