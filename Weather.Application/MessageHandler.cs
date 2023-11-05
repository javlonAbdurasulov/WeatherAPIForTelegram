using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Weather.Application.Services;
using Weather.Domain.Entities;

namespace Weather.Application
{
    public class MessageHandler : IUpdateHandler
    {
        public static int Count = 1;
        private readonly IWeatherServices _weatherServices;
        public static string NowCity = "Tashkent";
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
        //private async void CallBack(object sender, CallbackQueryEventArgs e)
        //{
        //    if (e.CallbackQuery.Data == "A bosildi")
        //        await client
        //           .SendTextMessageAsync(e.CallbackQuery.From.Id, e.CallbackQuery.Data);
        //}

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            ////5 soat ayirishim kere ekan
            ///avtomatikal qildim
            ///
            //if (update.Message.Text == "Today")
            if (update.Message != null && update.Message.Text == "Погода на сегодня")
            {
                await Console.Out.WriteLineAsync("--------"+Count+"------");
                Count++;

                string res = await _weatherServices.GetWeatherToday();

                //var sentMsg = await botClient.SendTextMessageAsync(chatId: update.Message.Chat.Id, text: "",replyMarkup: new ReplyKeyboardRemove());

                //await botClient.DeleteMessageAsync(chatId: update.Message.Chat.Id, sentMsg.MessageId);

                #region
                //var markups = new InlineKeyboardMarkup(
                //new InlineKeyboardButton[][]
                //    {
                //        new InlineKeyboardButton[]
                //        {
                //            InlineKeyboardButton
                //                .WithCallbackData(text: "A", callbackData: res),

                //            InlineKeyboardButton
                //                .WithCallbackData(text: "B", callbackData: "B bosildi")
                //        },
                //        new InlineKeyboardButton[]
                //        {
                //            InlineKeyboardButton
                //                .WithCallbackData(text: "C", callbackData: "C bosildi"),
                //            InlineKeyboardButton
                //                .WithCallbackData(text: "D", callbackData: "D bosildi")
                //        }
                //    }
                //);

                //await botClient.SendTextMessageAsync(
                //    chatId: update.Message.Chat.Id,
                //    text: "Ob-Havo",
                //    replyMarkup: markups
                //);
                #endregion

                await botClient.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    text: res
                );

            }
            else if(update.Message != null && update.Message.Text == "Погода на неделю")
            {
                //var sentMsg = await botClient.SendTextMessageAsync(chatId: update.Message.Chat.Id, text: "", replyMarkup: new ReplyKeyboardRemove());

                //await botClient.DeleteMessageAsync(chatId: update.Message.Chat.Id, sentMsg.MessageId);
                var res = await _weatherServices.GetWeatherWeek();
                await botClient.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id, 
                    text: res
                    );

            }
            else if(update.Message != null && update.Message.Text == "Поменять город")
            {
                //var sentMsg = await botClient.SendTextMessageAsync(chatId: update.Message.Chat.Id, text: "",replyMarkup: new ReplyKeyboardRemove());

                //await botClient.DeleteMessageAsync(chatId: update.Message.Chat.Id, sentMsg.MessageId);


                await botClient.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    text: $"Ваш Город: {NowCity}\n\nВыберите город:",
                    replyMarkup: GetButtonsCity()
                    );
            }
            else if(update.Message != null && update.Message.Text == "menu")
            {
                await botClient.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    text: "Menu:",
                    replyMarkup: GetButtons()
                );
                //await botClient.SendTextMessageAsync(
                //    chatId: update.Message.Chat.Id,
                //    text: "Share your contact & location",
                //    replyMarkup: new ReplyKeyboardMarkup(
                //        new[] { KeyboardButton.WithRequestContact("Share Contact"),
                //            KeyboardButton.WithRequestLocation("Share Location") }
                //    )
                //);
            }
            else if(update.Message != null && 
                    update.Message.Text == "Tashkent" ||
                    update.Message.Text == "Samarkand" ||
                    update.Message.Text == "Bukhara" 
                )
            {
                //var sentMsg = await botClient.SendTextMessageAsync(chatId: update.Message.Chat.Id, text: "", replyMarkup: new ReplyKeyboardRemove());

                //await botClient.DeleteMessageAsync(chatId: update.Message.Chat.Id, sentMsg.MessageId);

                var res = await _weatherServices.ChangeCity(update.Message.Text);
                string City = res;
                NowCity = City;
                await botClient.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    text: "Your City Now:  "+City
                );


            }

        }

        private IReplyMarkup? GetButtons()
        {
            var buttons = new List<List<KeyboardButton>>
            {
                new List<KeyboardButton>
                {
                    new KeyboardButton("Погода на сегодня"),
                    new KeyboardButton("Погода на неделю"),
                },
                new List<KeyboardButton>
                {
                    new KeyboardButton("Поменять город"),
                    new KeyboardButton("??"),
                }
            };
            return new ReplyKeyboardMarkup(buttons);
        }
        private IReplyMarkup? GetButtonsCity()
        {
            var buttons = new List<List<KeyboardButton>>
            {
                new List<KeyboardButton>
                {
                    new KeyboardButton("Tashkent"),
                    new KeyboardButton("Samarkand"),
                },
                new List<KeyboardButton>
                {
                    new KeyboardButton("Bukhara"),
                    new KeyboardButton("menu"),
                }
            };
            return new ReplyKeyboardMarkup(buttons);
        }
    }
}
