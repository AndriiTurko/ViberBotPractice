using System.Text.Json.Serialization;
using Viber.Bot.NetCore.Models;

namespace practice.DAL.Models
{
    public class MyMessageBase : ViberMessage.MessageBase
    {
        [JsonConstructor]
        public MyMessageBase(string type) : base(type)
        {
        }
    }
}
