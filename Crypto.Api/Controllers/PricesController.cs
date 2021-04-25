using Microsoft.AspNetCore.Mvc;
using Crypto.Api.Services;
using Crypto.Api.Utils;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Crypto.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PricesController : ControllerBase
    {
        private readonly IPriceService _priceService;

        public PricesController(IPriceService priceService)
        {
            priceService.ThrowIfNull("priceService");
;            _priceService = priceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLastPriceAsync()
        {
            // Retrieve userId from authentification
            var userKey = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var pricesResponse = await _priceService.GetLastPriceAsync(userKey);

            if (!pricesResponse.Success)
                return BadRequest(pricesResponse.Message);
            
            return Ok(pricesResponse);
        }
    }
}
