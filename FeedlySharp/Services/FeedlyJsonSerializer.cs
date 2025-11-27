using System.Text.Json;
using System.Text.Json.Serialization;
using FeedlySharp.Json;
using FeedlySharp.Models;

namespace FeedlySharp.Services;

/// <summary>
/// JSON serialization configuration for Feedly API with source generation support
/// </summary>
public static class FeedlyJsonSerializer
{
    private static readonly JsonSerializerOptions _options = new()
    {
        TypeInfoResolver = FeedlyJsonContext.Default,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNameCaseInsensitive = true,
        WriteIndented = false
    };

    static FeedlyJsonSerializer()
    {
        _options.Converters.Add(new DateTimeOffsetConverter());
        _options.Converters.Add(new NullableDateTimeOffsetConverter());
    }

    public static JsonSerializerOptions Options => _options;

    /// <summary>
    /// Custom converter for Feedly's microsecond epoch timestamps
    /// </summary>
    public class DateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        private static readonly DateTimeOffset Epoch = new(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);

        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return default;

            if (reader.TokenType == JsonTokenType.Number)
            {
                var milliseconds = reader.GetInt64();
                return Epoch.AddMilliseconds(milliseconds);
            }

            if (reader.TokenType == JsonTokenType.String && long.TryParse(reader.GetString(), out var ms))
            {
                return Epoch.AddMilliseconds(ms);
            }

            throw new JsonException($"Unexpected token type {reader.TokenType} when parsing DateTimeOffset");
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            var milliseconds = (long)(value - Epoch).TotalMilliseconds;
            writer.WriteNumberValue(milliseconds);
        }
    }

    /// <summary>
    /// Nullable DateTimeOffset converter
    /// </summary>
    public class NullableDateTimeOffsetConverter : JsonConverter<DateTimeOffset?>
    {
        private static readonly DateTimeOffset Epoch = new(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
        private static readonly DateTimeOffsetConverter _converter = new();

        public override DateTimeOffset? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;

            return _converter.Read(ref reader, typeof(DateTimeOffset), options);
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset? value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
                return;
            }

            _converter.Write(writer, value.Value, options);
        }
    }
}
