using System.Text.Json.Serialization;

namespace practice.DAL.Models
{
    public class MyTextMessage : MyMessageBase
    {
        [JsonPropertyName("text")]
        public string? Text { get; set; }

        public MyTextMessage()
            : base("text")
        {
        }
    }
}
