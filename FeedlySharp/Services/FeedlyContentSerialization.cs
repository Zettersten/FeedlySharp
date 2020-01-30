using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace FeedlySharp.Services
{
    public class FeedlyContentSerialization
    {
        public static JsonSerializerSettings SerializerSettings
        {
            get
            {
                var defaultSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    DefaultValueHandling = DefaultValueHandling.Include,
                    TypeNameHandling = TypeNameHandling.None,
                    NullValueHandling = NullValueHandling.Ignore,
                    Formatting = Formatting.None,
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
                };

                defaultSettings.Converters.Add(new MicrosecondEpochConverter());

                return defaultSettings;
            }
        }

        public class MicrosecondEpochConverter : DateTimeConverterBase
        {
            public override bool CanConvert(Type objectType)
            {
                var isConvertable = objectType == typeof(DateTimeOffset) || objectType == typeof(Nullable<DateTimeOffset>);

                return isConvertable;
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                if (reader.Value == null)
                {
                    return null;
                }

                if (!reader.Value.GetType().Equals(typeof(long)))
                {
                    return null;
                }

                var valueAsLong = (long)reader.Value;

                var newValue = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(valueAsLong);

                DateTimeOffset resultValue = newValue;

                return resultValue;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }
    }
}
