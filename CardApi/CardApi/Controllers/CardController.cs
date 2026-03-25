using Microsoft.AspNetCore.Mvc;

namespace CardApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardsController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateCard([FromBody] decimal creditLimit)
        {
            // TODO: Implement card creation logic

            return Ok();
        }
    }
}
