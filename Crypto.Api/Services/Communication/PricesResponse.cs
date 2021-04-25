using Crypto.Api.Domain.Models;

namespace Crypto.Api.Domain.Services.Communication
{
    public class PricesResponse : BaseResponse
    {
        public PriceData Prices { get; private set; }

        private PricesResponse(bool success, string message, PriceData prices) : base(success, message)
        {
            Prices = prices;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="PriceData">get PriceData.</param>
        /// <returns>Response.</returns>
        public PricesResponse(PriceData prices) : this(true, string.Empty, prices)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public PricesResponse(string message) : this(false, message, null)
        { }
    }
}