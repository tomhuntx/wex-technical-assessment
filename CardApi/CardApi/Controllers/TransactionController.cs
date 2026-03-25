using Microsoft.AspNetCore.Mvc;

namespace CardApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        [HttpPost]
        public IActionResult StoreTransaction([FromBody] object transaction)
        {
            // TODO: Define Transaction and replace above paramete
            // Create new Transaction object
            // Store transaction via PostgreSQL

            return Ok();
        }
    }
}
