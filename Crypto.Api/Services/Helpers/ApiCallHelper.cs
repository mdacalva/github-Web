using System;
using System.IO;
using System.Net;

namespace Crypto.Api.Helpers
{
    public static class ApiCallHelper
    {
        public static string Get(string apiUrl)
        {
            try
            {
                var responseString = "";
                var request = (HttpWebRequest)WebRequest.Create(apiUrl);
                request.Method = "GET";
                request.ContentType = "application/json";

                using (var response = request.GetResponse())
                {
                    using var reader = new StreamReader(response.GetResponseStream());
                    responseString = reader.ReadToEnd();
                }
                return responseString;
            }
            catch (Exception e)
            {
                // Do some logging
                throw new ApiCallException(apiUrl, $"{e.Message}");
            }
        }
    }
}
