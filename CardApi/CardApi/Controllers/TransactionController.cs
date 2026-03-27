using CardApi.Models.CardTransactions;
using CardApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CardApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionController(TransactionService transactionService, ExchangeRateService exchangeRateService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllTransactions()
    {
        var result = await transactionService.GetAllTransactions();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransaction([FromBody] CardTransactionRequest request)
    {
        var result = await transactionService.CreateTransaction(request);
        return Ok(result);
    }

    [HttpGet("{transactionId}/convert")]
    public async Task<IActionResult> GetTransactionInCurrency([FromRoute] Guid transactionId, [FromQuery] string currency)
    {
        var transaction = await transactionService.GetTransaction(transactionId);

        if (transaction == null)
            return NotFound(new { error = $"Transaction of ID \"{transactionId}\" not found." });

        var exchangeRate = await exchangeRateService.GetExchangeRate(currency, transaction.TransactionDate);

        if (exchangeRate == null)
            return BadRequest(new { error = "No exchange rate available within 6 months of transaction date." });

        var result = await transactionService.ConvertTransactionByExchangeRate(transaction, (decimal)exchangeRate, currency);
        
        return Ok(result);
    }
}
