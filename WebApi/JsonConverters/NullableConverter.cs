using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApi.JsonConverters
{
    /// <summary>
    /// Custom JSON converter that handles empty strings and converts them to null for nullable int types
    /// This solves the issue where frontends send "" instead of null for optional int fields
    /// </summary>
    public class NullableIntConverter : JsonConverter<int?>
    {
        public override int? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            if (reader.TokenType == JsonTokenType.String)
            {
                string stringValue = reader.GetString() ?? "";

                // Handle empty strings or whitespace - convert to null
                if (string.IsNullOrWhiteSpace(stringValue))
                {
                    return null;
                }

                // Try to parse the string as int
                if (int.TryParse(stringValue, out int value))
                {
                    return value;
                }

                // If parsing fails, return null instead of throwing an exception
                return null;
            }

            if (reader.TokenType == JsonTokenType.Number)
            {
                return reader.GetInt32();
            }

            // For any other token type, return null
            return null;
        }

        public override void Write(Utf8JsonWriter writer, int? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                writer.WriteNumberValue(value.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
        }
    }
}
