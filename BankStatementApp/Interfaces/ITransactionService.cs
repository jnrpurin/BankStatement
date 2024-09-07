using BankStatementApp.Models;

namespace BankStatementApp.Interfaces
{
    public interface ITransactionService
    {
        IEnumerable<BankTransaction> GetTransactionsByDays(int days);

        void AddTransaction(BankTransaction transaction);

        void UpdateTransaction(BankTransaction transaction);

        void DeleteTransaction(BankTransaction transaction);

        byte[] GeneratePdf(IEnumerable<BankTransaction> transactions);
    }
}
