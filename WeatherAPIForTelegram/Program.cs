
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

            builder.Services.AddSingleton<IWeatherServices, WeatherServices>();
            //builder.Services.AddScoped<IWeatherServices, WeatherServices>();
            //builder.Services.AddSingleton<MessageHandler>();
            
            TelegramBotClient client = new TelegramBotClient
                (builder.Configuration.GetSection("Tokens")["token1"]);
            //await client.DeleteWebhookAsync();
            //("6973642924:AAHvp441ILZGAwhNba6MVe-sJkenTXYNaPE");
            //client.StartReceiving(new MessageHandler());

            var serviceProvider = builder.Services.BuildServiceProvider();
            var messageHandler = new MessageHandler(serviceProvider.GetRequiredService<IWeatherServices>());

            client.StartReceiving(messageHandler);

            builder.Services.AddSingleton
                (client);
            
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