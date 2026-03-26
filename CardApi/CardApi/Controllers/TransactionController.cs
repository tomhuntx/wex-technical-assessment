using CardApi.Models;
using CardApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CardApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionController(TransactionService transactionService) : ControllerBase
{
    [HttpPost("{cardId}/transaction")]
    public async Task<IActionResult> CreateTransaction(Guid cardId, [FromBody] CardTransactionRequest request)
    {
        var result = await transactionService.CreateTransaction(cardId, request);
        return Ok(result);
    }
}
