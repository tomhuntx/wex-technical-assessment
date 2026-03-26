using CardApi.Models.Cards;
using CardApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CardApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CardController(CardService cardService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateCard([FromBody] CardCreateRequest request)
    {
        var card = await cardService.CreateCard(request);
        return Ok(card);
    }
}
