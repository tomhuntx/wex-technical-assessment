using CardApi.Models.Cards;
using CardApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CardApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CardController(CardService cardService, ExchangeRateService exchangeRateService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllCards()
    {
        var result = await cardService.GetAllCards();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCard([FromBody] CardCreateRequest request)
    {
        var card = await cardService.CreateCard(request);
        return Ok(card);
    }

    [HttpGet("{cardId}/balance")]
    public async Task<IActionResult> GetAvailableBalance(Guid cardId, [FromQuery] string? targetCurrency = null)
    {
        var card = await cardService.GetCard(cardId);

        if (card == null)
            return NotFound(new { error = $"Card of ID \"{cardId}\" not found." });

        var result = await cardService.GetCardBalance(card);

        if (!string.IsNullOrWhiteSpace(targetCurrency))
        {
            var rate = await exchangeRateService.GetExchangeRate(targetCurrency);

            if (rate == null)
                return BadRequest(new { error = $"No exchange rate available for {targetCurrency}." });

            result.Currency = targetCurrency;
            result.ExchangeRate = rate.Value;
            result.AvailableBalanceConverted = Math.Round(result.AvailableBalance * rate.Value, 2);
        }

        return Ok(result);
    }
}
