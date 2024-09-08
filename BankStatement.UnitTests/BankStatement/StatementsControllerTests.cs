using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BankStatementApp.Controllers;
using BankStatementApp.Interfaces;
using BankStatementApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace BankStatement.UnitTests.BankStatement
{
    public class StatementsControllerTests
    {
        private readonly Mock<ITransactionService> _serviceMock;
        private readonly Mock<ILogger<StatementController>> _loggerMock;
        private readonly StatementController _controller;

        public StatementsControllerTests()
        {
            _loggerMock = new Mock<ILogger<StatementController>>();
            _serviceMock = new Mock<ITransactionService>();
            _controller = new StatementController(_loggerMock.Object, _serviceMock.Object);
        }

        [Fact]
        public void GetAllTransactions_ShouldReturnOkResult()
        {
            // Arrange
            var transactions = new List<BankTransaction>
            {
                new BankTransaction { Date = DateTime.Now, TransactionType = "Depósito", Amount = 1000 }
            };

            _serviceMock.Setup(service => service.GetAll()).Returns(transactions);

            // Act
            var result = _controller.GetStatement(20);

            var okResult = Assert.IsType<OkObjectResult>(result);
            
            var anonymousObject = okResult.Value.GetType().GetProperty("transactions").GetValue(okResult.Value, null);
            var returnedTransactions = Assert.IsAssignableFrom<List<BankTransaction>>(anonymousObject);

            // Assert
            Assert.Single(returnedTransactions);
        }

        [Fact]
        public void GetAllTransactions_ShouldReturnNotFound_WhenNoTransactions()
        {
            // Arrange
            _serviceMock.Setup(service => service.GetAll()).Returns(new List<BankTransaction>());

            // Act
            var result = _controller.GetStatement(20);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);

            var messageProperty = notFoundResult.Value.GetType().GetProperty("message");
            var message = messageProperty?.GetValue(notFoundResult.Value, null)?.ToString();

            // Assert
            Assert.Equal("Nenhuma transação encontrada.", message);
        }
    }
}
