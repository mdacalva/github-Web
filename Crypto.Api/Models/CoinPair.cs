using System;

namespace Crypto.Api.Domain.Models
{
    [Serializable()]
    public class CoinPair
    {
        public CoinPair(int id, string name, string symbol, string baseCoin, string quoteCoin)
        {
            Id = id;
            Name = name;
            Symbol = symbol;
            BaseCoin = baseCoin;
            QuoteCoin = quoteCoin;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }
        public string Symbol { get; private set; }
        public string BaseCoin { get; private set; }
        public string QuoteCoin { get; private set; }

    }
}