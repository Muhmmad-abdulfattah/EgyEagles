using EgyEagles.Domain.Enitites;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyEagles.DAL.Context
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString,string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Company> Companies => _database.GetCollection<Company>("Companies");
        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
        public IMongoCollection<Vehicle> Vehicles => _database.GetCollection<Vehicle>("Vehicles");

        public IMongoCollection<T> GetCollection<T>(string name) => _database.GetCollection<T>(name);


    }
}
