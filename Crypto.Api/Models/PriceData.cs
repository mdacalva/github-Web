namespace Crypto.Api.Domain.Models
{
    public class PriceData
    {
        public PriceData(string symbol, decimal bid, decimal ask, decimal rate)
        {
            Symbol = symbol;
            Bid = bid;
            Ask = ask;
            Rate = rate;
        }

        public string Symbol { get; private set; }
        public decimal Bid { get; private set; }
        public decimal Ask { get; private set; }
        public decimal Rate { get; private set; }
    }
}