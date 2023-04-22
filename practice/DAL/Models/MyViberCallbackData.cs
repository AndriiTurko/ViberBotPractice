using System.Text.Json.Serialization;
using Viber.Bot.NetCore.Models;
using practice.BLL.Converters;

namespace practice.DAL.Models
{
    public class MyViberCallbackData : ViberCallbackData
    {
        [JsonConverter(typeof(ViberMessageConverter))]
        public new MyMessageBase? Message { get; set; }
    }
}
