using BankStatement.Data.Context;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankStatement.Data
{
    public abstract class RepositoryBase<T>
    {
        protected readonly IMongoDbContext _mongoDbContext;
        protected RepositoryBase(IMongoDbContext mongoDbContext)
        { 
            mongoDbContext = _mongoDbContext;
        }

        protected abstract string DefaultCollectionName {  get; }

        /// <summary>
        /// Identifies the collection which the repository will use. 
        /// By Default uses the name of the class, but can be overriden in derived repositories
        /// </summary>
        /// <returns>T collection from mongo db</returns>
        public virtual IMongoCollection<T> GetCollection()
        {
            return _mongoDbContext.GetCollection<T>(DefaultCollectionName);
        }

        public virtual bool DeleteOne(Expression<Func<T, Boolean>> expression)
        {
            return GetCollection().DeleteOne(expression).DeletedCount == 1;
        }

        public virtual IEnumerable<T> Find(FilterDefinition<T> filter)
        {
            return GetCollection().Find(filter).ToEnumerable();
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, Boolean>> expression)
        {
            return GetCollection().Find(expression).ToEnumerable();
        }
        
        public virtual T FindOne(FilterDefinition<T> filter)
        {
            return GetCollection().Find(filter).Limit(1).FirstOrDefault();
        }
        public virtual T FindOne(Expression<Func<T, Boolean>> expression)
        {
            return GetCollection().Find(expression).Limit(1).FirstOrDefault();
        }
    }
}
