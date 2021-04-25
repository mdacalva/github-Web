using Crypto.Api.Services.Contracts;

namespace Crypto.Api.Domain.Models.Helpers
{
    public static class TypesConverter
    {
        public static PriceData ToPriceData(CointreeApiPricesResult cointreeApiPricesResult)
        {
            return new PriceData($"{cointreeApiPricesResult.buy}/{cointreeApiPricesResult.sell}", cointreeApiPricesResult.bid, cointreeApiPricesResult.ask, cointreeApiPricesResult.rate);
        }
    }
}
