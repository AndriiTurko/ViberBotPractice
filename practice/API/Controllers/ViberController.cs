using Microsoft.AspNetCore.Mvc;
using Microsoft.Owin;
using Newtonsoft.Json;
using practice.BLL.Interfaces;
using practice.BLL.Services;
using practice.DAL.Models;
using System.Text;
using Viber.Bot;
using Viber.Bot.NetCore.Infrastructure;
using Viber.Bot.NetCore.Models;
using Viber.Bot.NetCore.RestApi;

namespace practice.API.Controllers
{
    [Route("/viber")]
    [ApiController]
    public class ViberController : ControllerBase
    {
        private readonly IViberBotApi _viberBotApi;
        private readonly IViberBotService _viberBotService;


        public ViberController(IViberBotApi viberBotApi, IViberBotService viberBotService)
        {
            _viberBotApi = viberBotApi;
            _viberBotService = viberBotService;
        }

        // The service sets a webhook automatically, but if you want sets him manually then use this
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _viberBotApi.SetWebHookAsync(new ViberWebHook.WebHookRequest("https://fcba-188-191-238-55.ngrok-free.app/viber"));

            if (response.Content.Status == ViberErrorCode.Ok)
            {
                return Ok("Viber-bot is active");
            }
            else
            {
                return BadRequest(response.Content.StatusMessage);
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] MyViberCallbackData data)
        //{
        //    try
        //    {
        //        var result = await _viberBotService.ManageIncomingViberCallbackData(_viberBotApi, data);
        //
        //        return StatusCode(200, result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}


        //[HttpPost]
        //public async Task<ActionResult> Webhook(IOwinContext context)
        //{
        //    string json;
        //    using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
        //    {
        //        json = await reader.ReadToEndAsync();
        //    }
        //
        //    var webhook = JsonConvert.DeserializeObject<ViberWebhook>(json);
        //
        //    foreach (var message in webhook.MessageEvents)
        //    {
        //        var text = message.Message.Text;
        //        var sender = message.Sender.Id;
        //
        //        // Use the extracted information to perform the desired actions
        //        // ...
        //    }
        //
        //    return Ok();
        //}
    }
}
