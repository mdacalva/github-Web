using Crypto.Api.Domain.Models;
using Crypto.Api.Domain.Services.Communication;
using System.Threading.Tasks;

namespace Crypto.Api.Domain.User
{
    public interface IUserContextService
    {
        Task<CoinPair> GetCoinPreferenceAsync(string userKey);

        CoinPair GetCoinPreference(string userKey);
        Task<UserCoinPreferenceResponse> SetCoinPreference(string userKey, string baseCoin);
    }
}