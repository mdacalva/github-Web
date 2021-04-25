using Crypto.Api.Domain.Models;
using Crypto.Api.Domain.Services.Communication;
using Crypto.Api.Services.Helpers;
using Crypto.Api.Utils;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Crypto.Api.Domain.User
{
    public class UserContextService : IUserContextService
    {
        private readonly CoinPair _defaultCoinPair = new CoinPair(1, "Bitcoin", "BTC/AUD", "BTC", "AUD");
        private readonly AppSettings _appSettings;

        // coins hardcoded for simplicity, store in a db
        private static readonly List<CoinPair> _coins = new List<CoinPair>
        {
            new CoinPair(1, "Bitcoin", "BTC/AUD", "BTC", "AUD"),
            new CoinPair(2, "Etherium", "ETH/AUD", "ETH", "AUD"),
            new CoinPair(3, "Ripple", "XRP/AUD", "XRP", "AUD")
        };

        public UserContextService(IOptions<AppSettings> appSettings)
        {
            appSettings.ThrowIfNull("appSettings"); 
            _appSettings = appSettings.Value;
        }

        public async Task<CoinPair> GetCoinPreferenceAsync(string userKey)
        {
            return await Task.Run(() => GetCoinPreference(userKey));
        }

        public CoinPair GetCoinPreference(string userKey)
        {
            return (CoinPair)ActionHelper.RunSafe("GetCoinPreference", () =>
            {
                return DoGetCoinPreference(userKey);
            });
        }

        private CoinPair DoGetCoinPreference(string userKey)
        {
            // this data should be retrieved from db call
            var filePath = GetTempFilePath(userKey);

            return File.Exists(filePath)
                ? SerializeCommon.DeserializeJson<CoinPair>(File.ReadAllText(filePath))
                : _defaultCoinPair;
        }

        public async Task<UserCoinPreferenceResponse> SetCoinPreference(string userKey, string baseCoin)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var coin = ValidateAndReturnCoinFromInput(baseCoin);
                    // This data should be saved in database
                    // As alternate solution we will be using json files in temp dir
                    var filePath = GetTempFilePath(userKey);
                    var serialized = SerializeCommon.SerializeJson(coin);
                    File.WriteAllText(filePath, serialized);

                    return new UserCoinPreferenceResponse(coin);
                }
                catch (Exception ex)
                {
                    // Do logging
                    return new UserCoinPreferenceResponse($"An error occurred when updating user coin preference: {ex.Message}");
                }
            });
        }

        private CoinPair ValidateAndReturnCoinFromInput(string baseCoin)
        {
            var coin = _coins.ToList().FirstOrDefault(c => c.BaseCoin == baseCoin);
            if (coin == null)
                throw new ArgumentException($"Coin '{baseCoin}' doesn't exist in our database.");

            return coin;
        }

        private string GetTempFilePath(string userKey)
        {
            return Path.Combine(AppContext.BaseDirectory, _appSettings.TemporaryDirectory, userKey);
        }
    }
}