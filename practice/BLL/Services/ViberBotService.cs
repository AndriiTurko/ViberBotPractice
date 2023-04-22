using practice.BLL.Interfaces;
using Viber.Bot.NetCore.Models;
using Microsoft.EntityFrameworkCore;
using Viber.Bot.NetCore.RestApi;
using practice.DAL.Interfaces;
using practice.DAL.Models;
using practice.BLL.Helpers;
using System.Text;
using Viber.Bot;

namespace practice.BLL.Services
{
    public class ViberBotService : IViberBotService
    {
        private readonly IUnitOfWork unitOfWork;

        public ViberBotService(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        //public async Task<string> ManageIncomingViberCallbackData(IViberBotApi _viberBotApi, MyViberCallbackData data)
        //{
        //    //var button = new KeyboardButton
        //    //{
        //    //    Text = "Click Me!",
        //    //    ActionType = KeyboardActionType.Reply,
        //    //    ActionBody = "Button Clicked!"
        //    //};
        //    //
        //    //var keyboard = new Keyboard
        //    //{
        //    //    Buttons = new List<KeyboardButton> { button }
        //    //};
        //    //
        //    //var message = new TextMessage
        //    //{
        //    //    Text = "Press the button to continue...",
        //    //    Keyboard = keyboard
        //    //};
        //
        //    var message = new ViberMessage.TextMessage
        //    {
        //        Text = "No text"
        //    };
        //
        //    
        //
        //    switch (data.Event)
        //    {
        //        case "message":
        //            var mess = data.Message as DAL.Models.TextMessage;
        //
        //            var imei = mess.Text;
        //
        //            var text = GetOverallInfoOnWalks(imei);
        //
        //            message = new ViberMessage.TextMessage()
        //            {
        //                Receiver = data.Sender.Id,
        //                Text = text
        //            };
        //
        //            break;
        //
        //        case "conversation_started":
        //
        //            var imeis = await unitOfWork.TrackLocations.GetAllRaw().Select(w => w.Imei).Distinct().ToListAsync();
        //
        //            foreach (var imeii in imeis)
        //            {
        //                await FillWalkTable(imeii);
        //            }
        //
        //            message = new ViberMessage.TextMessage()
        //            {
        //                Receiver = data.User.Id,
        //                Text = "Введіть IMEI"
        //            };
        //
        //            break;
        //    }
        //
        //    await _viberBotApi.SendMessageAsync<ViberResponse.SendMessageResponse>(message);
        //
        //    return message.Text;
        //}

        public async Task ManageIncomingViberCallbackData(IViberBotClient viberBotClient, CallbackData? data)
        {
            if (data == null) return;

            switch (data.Event)
            {
                case EventType.ConversationStarted:

                    var imeis = await unitOfWork.TrackLocations.GetAllRaw().Select(w => w.Imei).Distinct().ToListAsync();

                    foreach (var imeii in imeis)
                    {
                        await FillWalkTable(imeii);
                    }

                    var messageOnStartConversation = new TextMessage()
                    {
                        Receiver = data.User.Id,
                        Text = "Введіть IMEI"
                    };

                    await viberBotClient.SendTextMessageAsync(messageOnStartConversation);

                    break;

                case EventType.Message:
                    if (data.Message is not TextMessage incomingTextMessage)
                        return;

                    var imei = incomingTextMessage.Text;

                    var text = GetOverallInfoOnWalks(imei);

                    var infoOnImei = new TextMessage()
                    {
                        Receiver = data.Sender.Id,
                        Text = text
                    };

                    await viberBotClient.SendTextMessageAsync(infoOnImei);

                    var keyBoardMessage = new KeyboardMessage
                    {
                        Receiver = data.Sender.Id,
                        Keyboard = new Keyboard
                        {
                            Buttons = new[]
                            {
                                new KeyboardButton
                                {
                                    Text = "Top 10 walks",
                                    ActionBody = GetTopTenWalks(imei),
                                    ActionType = KeyboardActionType.Reply,
                                    
                                }
                            }
                        },
                    };

                    await viberBotClient.SendKeyboardMessageAsync(keyBoardMessage);

                    break;

                default:
                    break;
            }
        }

        private async Task FillWalkTable(string? imei)
        {
            var trackLocations = await unitOfWork.TrackLocations
                .GetAllRaw()
                .Where(tl => tl.Imei == imei)
                .OrderBy(tl => tl.date_track)
                .ToListAsync();

            var walks = CountHelper.GetWalks(trackLocations);

            //add walks to database
            foreach (var walk in walks)
            {
                var walkInfo = CountHelper.WalkInfo(walk);

                var random = new Random();

                var newWalk = new Walk
                {
                    Id = random.Next(int.MaxValue),
                    Imei = imei,
                    Name = $"Walk - {walks.IndexOf(walk)}",
                    Duration = walkInfo.Item1,
                    Distance = Convert.ToDecimal(walkInfo.Item2)
                };

                if (!unitOfWork.Walks.GetAllRaw().Any(w => 
                    w.Imei == newWalk.Imei && 
                    w.Name == newWalk.Name))
                {
                    await unitOfWork.Walks.CreateAsync(newWalk);
                }
            }

            await unitOfWork.SaveChangesAsync();
        }     

        private string GetOverallInfoOnWalks(string? imei)
        {
            var responseMessage = new StringBuilder();

            var walksRaw = unitOfWork.Walks.GetAllRaw().Where(w => w.Imei == imei);

            var totalWalks = (walksRaw.ToList()).Count;
            var totalKmWalked = walksRaw.Sum(w => w.Distance);
            var totalWalkTime = walksRaw.ToList()
                .Aggregate(TimeSpan.Zero, (acc, w) => acc.Add(w.Duration - DateTime.MinValue));

            responseMessage.Append($"Total walks: {totalWalks}");

            responseMessage.Append('\n');

            responseMessage.Append($"Total km walked: {totalKmWalked}");

            responseMessage.Append('\n');

            responseMessage.Append($"Total walk time: {totalWalkTime}");

            return responseMessage.ToString();
        }

        private string GetTopTenWalks(string? imei)
        {
            return "Your top walks";
        }
    }
}
