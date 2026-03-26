using CardApi.Models.Cards;
using CardApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CardApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CardController(CardService cardService) : ControllerBase
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
    public async Task<IActionResult> GetAvailableBalance(Guid cardId)
    {
        var card = await cardService.GetCard(cardId);

        if (card == null)
            return NotFound(new { error = $"Card of ID \"{cardId}\" not found." });

        var result = await cardService.GetCardBalance(card);
        return Ok(result);
    }
}
