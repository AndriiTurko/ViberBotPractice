using practice.DAL.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace practice.BLL.Converters
{
    public class ViberMessageConverter : JsonConverter<MyMessageBase>
    {
        public override MyMessageBase? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using JsonDocument document = JsonDocument.ParseValue(ref reader);
            var messageType = document.RootElement.GetProperty("type").GetString();

            return messageType switch
            {
                "text" => JsonSerializer.Deserialize<MyTextMessage>(document.RootElement.GetRawText(), options),
                _ => throw new ArgumentOutOfRangeException(nameof(messageType), messageType, "Unknown message type"),
            };
        }

        public override void Write(Utf8JsonWriter writer, MyMessageBase value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }

}
