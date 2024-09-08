using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;


namespace BankStatement.Data.Context
{
    public interface IMongoDbContext
    {
        IMongoCollection<T> GetCollection<T>(string collectioName);

        MongoClient GetMongoClient();
    }

    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _database;
        protected readonly MongoClient _client;

        public MongoDbContext(IConfiguration configuration)
        {
            _client = new MongoClient(configuration.GetConnectionString("MongoDb"));
            _database = _client.GetDatabase(configuration["DatabaseName"]);
        }

        public IMongoCollection<T> GetCollection<T>(string collectioName) => _database.GetCollection<T>(typeof(T).Name);

        public MongoClient GetMongoClient() => _client;
    }

}