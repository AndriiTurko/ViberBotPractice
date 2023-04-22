using practice.DAL.Models;
using Viber.Bot;
using Viber.Bot.NetCore.RestApi;

namespace practice.BLL.Interfaces
{
    public interface IViberBotService
    {
        //Task<string> ManageIncomingViberCallbackData(IViberBotApi _viberBotApi, MyViberCallbackData data);

        Task ManageIncomingViberCallbackData(IViberBotClient viberBotClient, CallbackData? data);
    }
}
