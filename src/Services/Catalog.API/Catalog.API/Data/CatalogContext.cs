using Catalog.API.Common.Dependency;
using Catalog.API.Common.SiteSettings;
using Catalog.API.Entities;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext , IScopedDependency
    {
        private readonly MongoDataBaseSetting _mongosetting;

        
        public CatalogContext(IConfiguration configuration)  // we use ioptionsnapshot for outer library or project
        {
            _mongosetting = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>().MongoDataBaseSetting;
            var client = new MongoClient(_mongosetting.ConnectionString);
            var database = client.GetDatabase(_mongosetting.DatabaseName);
            Products = database.GetCollection<Product>(_mongosetting.CollectionName);
            CatalogContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
