using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Weather.Application.Services;
using Weather.Domain.Entities;

namespace Weather.Application
{
    public class MessageHandler : IUpdateHandler
    {
        public static int Count = 1;
        private readonly IWeatherServices _weatherServices;
        //public IWeatherServices WeatherServices => _weatherServices;
        //public IWeatherServices _weatherServices{ get; set; }

        public MessageHandler(IWeatherServices weatherServices)
        {
            _weatherServices = weatherServices;
        }
        public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        //public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        //{
        //    string res = await _weatherServices.GetWeatherToday();
        //    if (update.Message.Text=="Weather Today")
        //    {
        //        botClient.SendTextMessageAsync(
        //            chatId: 5559328968,
        //            text: res
        //            );
        //    }

        //    return Task.CompletedTask;
        //}
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message.Text == "Weather Today")
            {
                await Console.Out.WriteLineAsync("--------"+Count+"------");
                Count++;
                string res = await _weatherServices.GetWeatherToday();

                await botClient.SendTextMessageAsync(
                    chatId: /*5559328968*/update.Message.Chat.Id,
                    text: res
                );
            }

            // Возвращайте задачу с помощью Task.CompletedTask, когда метод завершает работу
            //return;
        }

    }
}
