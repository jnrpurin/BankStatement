using BankStatementApp.Models;
using MongoDB.Bson;

namespace BankStatementApp.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<BankTransaction>> GetTransactionsByDays(int days);

        IEnumerable<BankTransaction> GetAll();

        BankTransaction GetTransactionById(ObjectId objectId);

        void AddTransaction(BankTransaction transaction);

        void UpdateTransaction(BankTransaction transaction);

        void DeleteTransaction(BankTransaction transaction);

        byte[] GeneratePdf(IEnumerable<BankTransaction> transactions);
    }
}
