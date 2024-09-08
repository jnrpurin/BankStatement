using BankStatementApp.Interfaces;
using BankStatementApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

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
        public async Task<IActionResult> GetStatement([FromQuery] int days)
        {
            if (days != 5 && days != 10 && days != 15 && days != 20)
            {
                return BadRequest(new { message = "Dias inválidos. Selecione entre 5, 10, 15 ou 20 dias." });
            }

            var transactions = await _transactionService.GetTransactionsByDays(days);
            if (!transactions.Any())
            {
                return NotFound(new { message = "Nenhuma transação encontrada." });
            }

            return Ok(new { transactions });
        }


        // GET /api/statements/pdf?days=5
        [HttpGet("pdf")]
        [Authorize]
        public async Task<IActionResult> GetStatementPdf([FromQuery] int days)
        {
            var transactions = await _transactionService.GetTransactionsByDays(days);
            if (!transactions.Any())
            {
                return NotFound(new { message = "Nenhuma transação encontrada para os últimos " + days + " dias." });
            }

            var pdfData = _transactionService.GeneratePdf(transactions);
            return File(pdfData, "application/pdf", $"extrato_{DateTime.Today.ToString()}_{days}_dias.pdf");
        }

        [HttpGet("GetAll")]
        //[Authorize]
        public IActionResult GetAllStatement()
        {
            var transactions = _transactionService.GetAll();
            if (!transactions.Any())
            {
                return NotFound(new { message = "Nenhuma transação encontrada." });
            }

            return Ok(new { transactions });
        }

        [HttpGet("GetById/{id}")]
        //[Authorize]
        public IActionResult GetById(ObjectId id)
        {
            var transaction = _transactionService.GetTransactionById(id);
            if (transaction == null)
            {
                return NotFound("Transação não econtrada.");
            }

            return Ok(new
            {
                Id = transaction.Id.ToString(),
                transaction.DateFormatted,
                transaction.Amount,
                transaction.TransactionType
            });
        }

        [HttpPost("create-transaction")]
        [Authorize]
        public IActionResult CreateTransaction([FromBody] BankTransaction transaction)
        {
            if (transaction == null)
            {
                return BadRequest("Transação inválida.");
            }

            _transactionService.AddTransaction(transaction);
            return CreatedAtAction(nameof(GetAllStatement), transaction);
        }

        [HttpDelete("delete-transaction/{id}")]
        [Authorize]
        public IActionResult DeleteTransaction(ObjectId id)
        {
            var transaction = _transactionService.GetTransactionById(id);
            if (transaction == null)
            {
                return NotFound("Transação não econtrada.");
            }

            _transactionService.DeleteTransaction(transaction);
            return NoContent();
        }
    }
}
