using BankStatement.Data.Interfaces;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BankStatementApp.Models
{
    public class BankTransaction : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        
        [BsonElement("date")]
        public DateTime Date { get; set; }

        public string DateFormatted => Date.ToString("dd/MM");

        [BsonElement("transactionType")]
        public required string TransactionType { get; set; }

        [BsonElement("amount")]
        public decimal Amount { get; set; } 
    }

}
