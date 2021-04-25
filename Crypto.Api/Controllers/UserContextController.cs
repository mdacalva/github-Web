using Microsoft.AspNetCore.Mvc;
using Crypto.Api.Utils;
using Crypto.Api.Domain.User;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Crypto.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserContextController : ControllerBase
    {
        private readonly IUserContextService _userContextManager;

        public UserContextController(IUserContextService userContextManager)
        {
            userContextManager.ThrowIfNull("userContextManager");
            _userContextManager = userContextManager;
        }

        // Method implemented for testing purposes
        [HttpGet]
        public async Task<IActionResult> GetContextAsync()
        {
            // Retrieve userId from authentification
            var userKey = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var coin = await _userContextManager.GetCoinPreferenceAsync(userKey);

            if (coin == null)
            {
                var userName = User.FindFirst(ClaimTypes.Name)?.Value;
                return BadRequest(new { message = $"Cannot request user context for user '{userName}'" });
            }

            return Ok(coin);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(string baseCoin)
        {
            // Retrieve userId from authentification
            var userKey = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _userContextManager.SetCoinPreference(userKey, baseCoin);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(baseCoin);
        }
    }
}
