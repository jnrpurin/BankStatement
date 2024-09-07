namespace BankStatementApp.Models
{
    public class Transaction
    {
        public DateTime Date { get; set; } //(dd/MM)
        public string TransactionType { get; set; } 
        public decimal Amount { get; set; } 
    }

}
