using BankStatementApp.Interfaces;
using BankStatementApp.Models;
using MongoDB.Bson;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace BankStatementApp.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repository;

        public TransactionService(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<BankTransaction> GetAll()
        {
            return _repository.GetBankTransactions();
        }

        public BankTransaction GetTransactionById(ObjectId objectId)
        {
            return _repository.GetBankTransactions().FirstOrDefault(t => t.Id.Equals(objectId));
        }

        public async Task<IEnumerable<BankTransaction>> GetTransactionsByDays(int days)
        {
            try
            {
                var startDate = DateTime.Now.AddDays(-days);
                return await _repository.GetBankTransactions(startDate, DateTime.Now);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro: As transações não foram recuperadas.", ex);
            }
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
            using (var stream = new MemoryStream())
            {
                var document = new PdfDocument();
                var page = document.AddPage();
                var gfx = XGraphics.FromPdfPage(page);
                var font = new XFont("Verdana", 12);
                var yOffset = 20;

                var content = new List<string>{
                    "Extrato Bancário",
                    "================"
                };
                foreach (var t in transactions)
                {
                    content.Add($"{t.DateFormatted} - {t.TransactionType} - {t.Amount:C}");
                }
        
                foreach (var item in content)
                {
                    gfx.DrawString(item, font, XBrushes.Black,
                        new XRect(0, yOffset, page.Width, page.Height),
                        XStringFormats.TopLeft);

                    yOffset += 20;
                }

                document.Save(stream);
                stream.Position = 0;

                var fileContent = stream.ToArray();

                return fileContent;
            }
        }
    }
}
