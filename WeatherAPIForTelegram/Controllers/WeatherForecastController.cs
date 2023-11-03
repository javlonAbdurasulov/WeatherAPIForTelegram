using Microsoft.AspNetCore.Mvc;
using Weather.Application.Services;
using Weather.Domain.Entities;

namespace WeatherAPIForTelegram.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherServices _weatherServices;

        public WeatherForecastController(IWeatherServices weatherServices,ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _weatherServices = weatherServices;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<string> GetAsync(string city="Tashkent")
        {
            #region
            //string token = "6973642924:AAHvp441ILZGAwhNba6MVe-sJkenTXYNaPE";
            //long chat_id = 5559328968;
            //string message = Console.ReadLine();
            //using HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri($"https://api.telegram.org/bot{token}/sendMessage?chat_id={chat_id}&&text={message}");
            //client.BaseAddress = new Uri($"https://api.telegram.org/bot{token}/UserProfilePhotos?user_id={chat_id}");

            //client.BaseAddress = new Uri($"https://api.open-meteo.com/v1/forecast?latitude=41.2647&longitude=69.2163&hourly=temperature_2m");

            //var res = await client.GetAsync("");
            //var res2 = await res.Content.ReadFromJsonAsync<WeatherForecast>();
            #endregion

            var weather = await _weatherServices.GetWeatherIN7();
            int startIndex = 0; 
            int takeCount = 7; 


            //if (startIndex >= 0 && takeCount < weather.hourly.temperature_2m.Count && startIndex <= takeCount)
            //{
                double maxMorningTemperature = weather.hourly.temperature_2m
                    .Skip(startIndex)
                    .Take(takeCount)
                    .Average();
            maxMorningTemperature = Math.Round(maxMorningTemperature);
            //}
            startIndex +=takeCount;
            takeCount = 17;
            //if (startIndex >= 0 && takeCount < weather.hourly.temperature_2m.Count && startIndex <= takeCount)
            //{
            double maxNightTemperature = weather.hourly.temperature_2m
                    .Skip(startIndex)
                    .Take(takeCount)
                    .Max();
            maxNightTemperature = Math.Round(maxNightTemperature);
            //}
            //var morning = weather.hourly.time.Where(x =>  );
            string res = $"Day:{maxNightTemperature} Night:{maxMorningTemperature}";
            return res;
        }
    }
}