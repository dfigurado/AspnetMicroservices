using Catalog.Persistence.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    internal class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetConnectionString("DatabaseSettings:ConnectionString"))
        }
        public IMongoCollection<Product> Products => throw new NotImplementedException();
    }
}
