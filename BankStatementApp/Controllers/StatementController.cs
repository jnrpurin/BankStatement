using BankStatementApp.Interfaces;
using BankStatementApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BankStatementApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatementController : Controller
    {
        private readonly ILogger<StatementController> _logger;
        private readonly ITransactionService _transactionService;

        public StatementController(ILogger<StatementController> logger, ITransactionService transactionService)
        {
            _transactionService = transactionService;
            _logger = logger;
        }

        [HttpGet("extract/{days}")]
        public IActionResult GetStatement(int days)
        {
            return NotFound("Em desenvolvimento.");
        }

        [HttpGet("extract/pdf/{days}")]
        public IActionResult GetStatementPdf(int days)
        {
            return NotFound("Em desenvolvimento.");
        }
    }
}
