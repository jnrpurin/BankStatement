using BankStatementApp.Interfaces;
using BankStatementApp.Models;
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
        public IActionResult GetStatement([FromQuery] int days)
        {
            return BadRequest(new { message = "Em desenvolvimento." });
        }


        // GET /api/statements/pdf?days=5
        [HttpGet("pdf")]
        public IActionResult GetStatementPdf([FromQuery] int days)
        {
            return BadRequest(new { message = "Em desenvolvimento." });
        }
    }
}
