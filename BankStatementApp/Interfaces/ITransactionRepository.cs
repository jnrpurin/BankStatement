using BankStatementApp.Models;

namespace BankStatementApp.Interfaces
{
    public interface ITransactionRepository
    {

        IEnumerable<BankTransaction> GetBankTransactions(DateTime startDate, DateTime endDate);

        void InsertBankTransaction(BankTransaction transaction);

        void UpdateBankTransaction(BankTransaction transaction);

        void DeleteBankTransaction(BankTransaction transaction);

    }
}
