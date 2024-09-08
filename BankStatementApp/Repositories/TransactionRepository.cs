using BankStatement.Data;
using BankStatement.Data.Context;
using BankStatementApp.Interfaces;
using BankStatementApp.Models;
using MongoDB.Driver;

namespace BankStatementApp.Repositories
{
    public class TransactionRepository : MongoDbRepository<BankTransaction>, ITransactionRepository
    {
        public TransactionRepository(IMongoDbContext mongoDbContext) : base(mongoDbContext)
        {
        }

        protected override string DefaultCollectionName => "BankTransaction";

        public IEnumerable<BankTransaction> GetBankTransactions()
        {
            return GetMany(_ => true);
        }

        public IEnumerable<BankTransaction> GetBankTransactions(DateTime startDate, DateTime endDate)
        {
            try
            {
                var filter = Builders<BankTransaction>.Filter.And(
                    Builders<BankTransaction>.Filter.Gte(t => t.Date, startDate),
                    Builders<BankTransaction>.Filter.Lte(t => t.Date, endDate)
                );

                return GetMany(filter);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void InsertBankTransaction(BankTransaction transaction)
        {
            Create(transaction);
        }

        public void UpdateBankTransaction(BankTransaction transaction)
        {
            Update(transaction);
        }

        public void DeleteBankTransaction(BankTransaction transaction)
        {
            Delete(transaction.Id);
        }

    }
}
