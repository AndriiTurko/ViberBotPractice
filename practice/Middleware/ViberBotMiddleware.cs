using Newtonsoft.Json;
using Viber.Bot;
using practice.BLL.Interfaces;

namespace practice.Middleware
{
    public class ViberWebhookMiddleware
    {
        private readonly RequestDelegate _next;

        private ViberBotClient _viberBotClient;

        //private readonly IViberBotService _viberBotService;

        private string? _authToken;
        private string? _webhookUrl;

        private List<EventType> eventTypes = new() { EventType.ConversationStarted, EventType.Message };

        public ViberWebhookMiddleware(RequestDelegate next)
        {
            _next = next;

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            if (config != null)
            {
                _authToken = config.GetSection("ViberBot")["Token"];
                _webhookUrl = config.GetSection("ViberBot")["Webhook"];
            }

            _viberBotClient = new ViberBotClient(_authToken);

            _viberBotClient.SetWebhookAsync(_webhookUrl, eventTypes);
        }

        public async Task InvokeAsync(HttpContext context, IViberBotService _viberBotService)
        {
            var isEndpointValid = context.Request.Path == "/viber"; // compare context.Request.Path and webhook url

            if (!isEndpointValid)
            {
                await _next(context);
                return;
            }

            var body = new StreamReader(context.Request.Body).ReadToEndAsync();

            var callbackData = JsonConvert.DeserializeObject<CallbackData>(body.Result);
            // process callback

            if (callbackData != null)
            {
                await _viberBotService.ManageIncomingViberCallbackData(_viberBotClient, callbackData);
            }
        }
    }
}
