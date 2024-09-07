using BankStatementApp.Interfaces;
using BankStatementApp.Models;

namespace BankStatementApp.Services
{
    public class TransactionService: ITransactionService
    {
        private readonly ITransactionRepository _repository;

        public TransactionService(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<BankTransaction> GetTransactionsByDays(int days)
        {
            var startDate = DateTime.Now.AddDays(-days);
            return _repository.GetBankTransactions(startDate, DateTime.Now);
        }

        public void AddTransaction(BankTransaction transaction)
        {
            _repository.InsertBankTransaction(transaction);
        }

        public void UpdateTransaction(BankTransaction transaction)
        {
            _repository.UpdateBankTransaction(transaction);
        }

        public void DeleteTransaction(BankTransaction transaction) 
        {
            _repository.DeleteBankTransaction(transaction);
        }

        public byte[] GeneratePdf(IEnumerable<BankTransaction> transactions)
        {
            using var ms = new MemoryStream();
            using var writer = new StreamWriter(ms);
            writer.WriteLine("Extrato Bancário");
            writer.WriteLine("================");
            foreach (var t in transactions)
            {
                writer.WriteLine($"{t.Date:dd/MM} - {t.TransactionType} - {t.Amount:C}");
            }
            writer.Flush();
            return ms.ToArray();
        }
    }
}
