using System;
using System.Threading.Tasks;

namespace Crypto.Api.Services.Helpers
{
    public class ActionHelper
    {
        public static object RunSafe<TReturn>(string actionDescription, Func<TReturn> action)
        {
            try
            {
                var result = action();
                return result;
            }
            catch (Exception e)
            {
                // Do some loggin with actionDescription and exception
                return null;
            }
        }
    }
}
