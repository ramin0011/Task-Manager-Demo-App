using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace TaskManager.Infrastructure.Data
{
    public class MongoDbContext : IMongoDbContext
    {
        private IMongoDatabase database;
        private readonly string connectionString;
        private readonly string dbName;

     
        public MongoDbContext(string connectionstring, string dbName)
        {
            this.connectionString = connectionstring;
            this.dbName = dbName;
        }

        public IMongoDatabase Database => database ??= new MongoClient(this.connectionString).GetDatabase(dbName);

    }
}
