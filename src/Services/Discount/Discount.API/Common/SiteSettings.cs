namespace Discount.API.Common
{
    public class SiteSettings
    {
        public PostgresDbSettings postgresDbSettings;
    }
    public class PostgresDbSettings
    {
      public string PostgreDbConnection { get; set; }
    }
}
