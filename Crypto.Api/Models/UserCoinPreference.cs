namespace Crypto.Api.Domain.Models
{
    public class UserCoinPreference
    {
        public UserCoinPreference(string userKey, CoinPair coin)
        {
            UserKey = userKey;
            Coin = coin;
        }

        public string UserKey { get; private set; }
        public CoinPair Coin { get; private set; }

    }
}