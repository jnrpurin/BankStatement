using BankStatement.Data.Context;
using BankStatement.Data.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankStatement.Data
{
    public interface IRepository<T> where T : class, IEntity
    {
        void Create(T entity);

        void Update(T entity);

        bool Delete(Guid id);

        T Get(Guid Id);

        IEnumerable<T> GetMany(FilterDefinition<T> filter);
    }

    public class MongoDbRepository<T>: RepositoryBase<T>, IRepository<T> where T : class, IEntity
    {
        public MongoDbRepository(IMongoDbContext context) : base(context)
        { 
        }

        protected override string DefaultCollectionName
        {
            get
            {
                return typeof(T).Name.ToLower(); 
            }
        }

        public virtual void Create(T entity)
        {
            GetCollection().InsertOne(entity);
        }

        public virtual bool Delete(Guid id)
        {
            return DeleteOne(x => x.Id == id);
        }

        public virtual T Get(Guid Id)
        {
            return FindOne(x => x.Id == Id);
        }
        public virtual IEnumerable<T> GetMany(FilterDefinition<T> filter)
        {
            return GetMany(filter);
        }

        public virtual void Update(T entity)
        {
            GetCollection().ReplaceOne(x => x.Id == entity.Id, entity);
        }
    }
}
