using CardApi.Data;
using CardApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CardApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CardController(CardService cardService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateCard([FromBody] decimal creditLimit)
    {
        var card = await cardService.CreateCard(creditLimit);
        return Ok(card);
    }
}
