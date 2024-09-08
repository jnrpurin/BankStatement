using BankStatement.Data.Context;
using BankStatement.Data.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankStatement.Data
{
    public interface IRepository<T> where T : class, IEntity
    {
        void Create(T entity);

        void Update(T entity);

        bool Delete(ObjectId id);

        T Get(ObjectId Id);

        IEnumerable<T> GetMany(FilterDefinition<T> filter);

        IEnumerable<T> GetMany(Expression<Func<T, Boolean>> expression);
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

        public virtual bool Delete(ObjectId id)
        {
            return DeleteOne(x => x.Id == id);
        }

        public virtual T Get(ObjectId Id)
        {
            return FindOne(x => x.Id == Id);
        }

        public virtual IEnumerable<T> GetMany(FilterDefinition<T> filter)
        {
            return Find(filter);
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, Boolean>> expression)
        {
            return Find(expression);
        }

        public virtual void Update(T entity)
        {
            GetCollection().ReplaceOne(x => x.Id == entity.Id, entity);
        }
    }
}
