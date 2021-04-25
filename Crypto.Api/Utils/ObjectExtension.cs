using System;

namespace Crypto.Api.Utils
{
    public static class ObjectExtension
    {
        public static void ThrowIfNull<T>(this T data, string name) where T : class
        {
            if (data == null)
                throw new ArgumentNullException(name);
        }

    }
}
