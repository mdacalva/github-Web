using System;
using Crypto.Api.Domain.Models;
using Crypto.Api.Domain.User;
using Crypto.Api.Helpers;
using Crypto.Api.Services.Contracts;
using Crypto.Api.Utils;
using Crypto.Api.Domain.Models.Helpers;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Crypto.Api.Domain.Services.Communication;

namespace Crypto.Api.Services
{
    public class PriceService : IPriceService
    {
        private readonly IUserContextService _userContextManager;
        private readonly AppSettings _appSettings;

        public PriceService(IUserContextService userContextManager, IOptions<AppSettings> appSettings)
        {
            userContextManager.ThrowIfNull("userContextManager");
            appSettings.ThrowIfNull("appSettings");
            _userContextManager = userContextManager;
            _appSettings = appSettings.Value;
        }

        public async Task<PricesResponse> GetLastPriceAsync(string userKey)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var coin = _userContextManager.GetCoinPreference(userKey);
                    var dataAsJson = ApiCallHelper.Get($"{_appSettings.CointreeApiUrl}/{coin.BaseCoin}");
                    var priceResult = SerializeCommon.DeserializeJson<CointreeApiPricesResult>(dataAsJson);
                    return new PricesResponse(TypesConverter.ToPriceData(priceResult));
                }
                // Possibility to overwrite exception handling message from thirdparty web api call
                catch (ApiCallException ex)
                {
                    // Do some logging
                    return new PricesResponse($"Web Api call to Url '{ex.Url}' returned exception: {ex.Message}"); ;
                }
                catch (Exception ex)
                {
                    // Do some logging
                    return new PricesResponse(ex.Message);
                }
            });
        }
    }
}