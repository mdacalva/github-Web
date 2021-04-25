using Crypto.Api.Domain.Models;

namespace Crypto.Api.Domain.Services.Communication
{
    public class UserCoinPreferenceResponse : BaseResponse
    {
        public CoinPair CoinPair { get; private set; }

        private UserCoinPreferenceResponse(bool success, string message, CoinPair coinPair) : base(success, message)
        {
            CoinPair = coinPair;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="userCoinPreference">Saved userCoinPreference.</param>
        /// <returns>Response.</returns>
        public UserCoinPreferenceResponse(CoinPair coinPair) : this(true, string.Empty, coinPair)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public UserCoinPreferenceResponse(string message) : this(false, message, null)
        { }
    }
}