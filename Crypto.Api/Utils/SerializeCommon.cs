using Newtonsoft.Json;
using System;
using System.IO;

namespace Crypto.Api.Utils
{
    public static class SerializeCommon
    {
        public static TType DeserializeJson<TType>(string serialized)
        {
            Func<JsonSerializer, JsonTextReader, TType> getter =
                (serializer, json) => serializer.Deserialize<TType>(json);
            return DoDeserializeJson(serialized, getter);
        }

        private static TType DoDeserializeJson<TType>(string serialized,
            Func<JsonSerializer, JsonTextReader, TType> getter)
        {
            using var stream = new StringReader(serialized);
            using var json = new JsonTextReader(stream);
            var serializer = GetSerialiser();
            return getter(serializer, json);
        }

        public static string SerializeJson(object obj)
        {
            return SerializeJson(obj, Formatting.Indented);
        }

        public static string SerializeJson(object obj, Formatting formatting)
        {
            using var stream = new StringWriter();
            using var json = new JsonTextWriter(stream);
            var serializer = GetSerialiser();
            serializer.Formatting = formatting;
            serializer.Serialize(json, obj);
            return stream.GetStringBuilder().ToString();
        }

        public static JsonSerializer GetSerialiser()
        {
            return new JsonSerializer();
        }

    }
}
