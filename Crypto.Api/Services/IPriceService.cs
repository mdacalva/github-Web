using Crypto.Api.Domain.Models;
using Crypto.Api.Domain.Services.Communication;
using System.Threading.Tasks;

namespace Crypto.Api.Services
{
    public interface IPriceService
    {
        Task<PricesResponse> GetLastPriceAsync(string userKey);
    }
}