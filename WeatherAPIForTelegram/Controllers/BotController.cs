using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace WeatherAPIForTelegram.Controllers
{
    //[Route("api/bot")]
    //[ApiController]
    public class BotController : ControllerBase
    {
        
        private readonly TelegramBotClient _botClient;
        public BotController(TelegramBotClient client)
        {
            _botClient = client;            
        }
        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] Update update)
        //{
        //    if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
        //    {
        //        await _botClient.SendTextMessageAsync(update.Message.From.Id,"sss");
        //    }

        //    return Ok();
        //}
    }
}
