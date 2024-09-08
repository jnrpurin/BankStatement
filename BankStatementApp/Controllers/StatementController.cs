using BankStatementApp.Interfaces;
using BankStatementApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BankStatementApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatementController : ControllerBase
    {
        private readonly ILogger<StatementController> _logger;
        private readonly ITransactionService _transactionService;

        public StatementController(ILogger<StatementController> logger, ITransactionService transactionService)
        {
            _transactionService = transactionService;
            _logger = logger;
        }

        // GET /api/statements?days=5
        [HttpGet]
        [Authorize]
        public IActionResult GetStatement([FromQuery] int days)
        {
            if (days != 5 && days != 10 && days != 15 && days != 20)
            {
                return BadRequest(new { message = "Dias inválidos. Selecione entre 5, 10, 15 ou 20 dias." });
            }

            //var transactions = _transactionService.GetTransactionsByDays(days);
            var transactions = _transactionService.GetAll();
            if (!transactions.Any())
            {
                return NotFound(new { message = "Nenhuma transação encontrada." });
            }

            return Ok(new { transactions });
        }


        // GET /api/statements/pdf?days=5
        [HttpGet("pdf")]
        [Authorize]
        public IActionResult GetStatementPdf([FromQuery] int days)
        {
            var transactions = _transactionService.GetTransactionsByDays(days);
            if (!transactions.Any())
            {
                return NotFound(new { message = "Nenhuma transação encontrada para os últimos " + days + " dias." });
            }

            var pdfData = _transactionService.GeneratePdf(transactions);
            return File(pdfData, "application/pdf", $"extrato_{days}_dias.pdf");
        }
    }
}
