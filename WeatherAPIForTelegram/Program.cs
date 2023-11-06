
using Telegram.Bot;
using Weather.Application;
using Weather.Application.Services;

namespace WeatherAPIForTelegram
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddHttpClient();
            #region
            //builder.Services.AddSingleton<IWeatherServices, WeatherServices>();
            builder.Services.AddScoped<IWeatherServices, WeatherServices>();
            //builder.Services.AddSingleton<MessageHandler>();


            builder.Services.AddScoped<MessageHandler>();
            #endregion

            TelegramBotClient client = new TelegramBotClient
                (builder.Configuration.GetSection("Tokens")["token1"]);
            //await client.DeleteWebhookAsync();
            //("6973642924:AAHvp441ILZGAwhNba6MVe-sJkenTXYNaPE");
            //client.StartReceiving(new MessageHandler());

            #region
            var serviceProvider = builder.Services.BuildServiceProvider();
            var messageHandler = new MessageHandler(serviceProvider.GetRequiredService<IWeatherServices>());

            //var serviceProvider = builder.Services.BuildServiceProvider();
            //using (var scope = serviceProvider.CreateScope())
            //{
            //    var scopedServiceProvider = scope.ServiceProvider;
            //    var messageHandler = new MessageHandler(scopedServiceProvider.GetRequiredService<IWeatherServices>());

            //    client.StartReceiving(messageHandler);
            //}

            client.StartReceiving(messageHandler);

            builder.Services.AddSingleton
                (client);
            #endregion

            ////////////////////////////
            //builder.Services.AddSingleton<ITelegramBotClient>(client);
            //builder.Services.AddSingleton<IWeatherServices, WeatherServices>();
            //builder.Services.AddScoped<MessageHandler>();




            ////////////////////////////

            //(new TelegramBotClient(builder.Configuration.GetConnectionString("Token")));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}