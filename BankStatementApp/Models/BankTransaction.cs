using BankStatement.Data.Interfaces;

namespace BankStatementApp.Models
{
    public class BankTransaction : IEntity
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; } //(dd/MM)
        public required string TransactionType { get; set; } 
        public decimal Amount { get; set; } 
    }

}
