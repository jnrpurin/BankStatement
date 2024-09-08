using BankStatementApp.Models;

namespace BankStatementApp.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<BankTransaction>> GetBankTransactions(DateTime startDate, DateTime endDate);

        IEnumerable<BankTransaction> GetBankTransactions();

        void InsertBankTransaction(BankTransaction transaction);

        void UpdateBankTransaction(BankTransaction transaction);

        void DeleteBankTransaction(BankTransaction transaction);

    }
}
