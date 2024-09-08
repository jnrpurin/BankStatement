using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankStatementApp.Interfaces;
using BankStatementApp.Models;
using BankStatementApp.Services;
using Moq;

namespace BankStatement.UnitTests.BankStatement
{
    public class TransactionServiceTests
    {
        private readonly Mock<ITransactionRepository> _repositoryMock;
        private readonly TransactionService _transactionService;
        public TransactionServiceTests()
        {
            _repositoryMock = new Mock<ITransactionRepository>();
            _transactionService = new TransactionService(_repositoryMock.Object);
        }

        [Fact]
        public void GetBankTransactions_ShouldReturnAllTransactions()
        {
            // Arrange
            var transactions = new List<BankTransaction>
            {
                new BankTransaction { Date = DateTime.Now, TransactionType = "Depósito", Amount = 1000 },
                new BankTransaction { Date = DateTime.Now.AddDays(-1), TransactionType = "Saque", Amount = -500 }
            };

            _repositoryMock.Setup(repo => repo.GetBankTransactions()).Returns(transactions);

            // Act
            var result = _transactionService.GetAll();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal(1000, result.First().Amount);
        }
    }
}
