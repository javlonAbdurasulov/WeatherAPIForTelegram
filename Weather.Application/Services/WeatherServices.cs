using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Weather.Domain.Entities;

namespace Weather.Application.Services
{
    public class WeatherServices : IWeatherServices /*, IHttpClientService*/
    {
        private readonly IConfiguration _configuration;
        public string _uri;
        public static string NowCity = "Tashkent";
        private readonly IHttpClientFactory _httpClientFactory;

        public WeatherServices(IHttpClientFactory httpClientFactory,/*HttpClient httpClient, */IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _uri = _configuration.GetSection("BaseAddress")["Tashkent"];
        }
        //////////////////////////
        //public void Dispose()
        //{
        //    _httpClient.Dispose();
        //}
        //////////////////////////
        public async Task<WeatherForecast> GetWeatherIN7()
        {
            using var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_uri);

            var response = await httpClient.GetAsync("").ConfigureAwait(false);//используется для предотвращения
                                                 //захвата текущего контекста синхронизации, что может помочь избежать блокировок.
            var responseModel = await response.Content.ReadFromJsonAsync<WeatherForecast>();

            return responseModel;
        }

        public async Task<string> GetWeatherToday()
        {
            WeatherForecast weather = await GetWeatherIN7();
            string res = $"Погода будет классная, если солнышко улыбнется, улыбнитесь принцесса🌞\r\n\nА пока в {NowCity}\nDate: {DateTime.Now.ToShortDateString()}\n\n";
            for (int i = 1; i < 24; i += 3)
            {
                var temp = Math.Round(weather.hourly.temperature_2m[i]);
                res += $"Time: {i + 1}:00  <=>  {temp} °C\n";
            }
            return res;

            #region
            //int startIndex = 0;
            //int takeCount = 7;

            //double maxMorningTemperature = weather.hourly.temperature_2m
            //    .Skip(startIndex)
            //    .Take(takeCount)
            //    .Average();
            //maxMorningTemperature = Math.Round(maxMorningTemperature);

            //startIndex += takeCount;
            //takeCount = 17;

            //double maxNightTemperature = weather.hourly.temperature_2m
            //        .Skip(startIndex)
            //        .Take(takeCount)
            //        .Max();
            //maxNightTemperature = Math.Round(maxNightTemperature);

            //res = $"Day: {maxNightTemperature} °C \nNight: {maxMorningTemperature} °C";
            #endregion
        }
        public async Task<string> GetWeatherWeek()
        {
            WeatherForecast weather = await GetWeatherIN7();
            string res = $"City:  {NowCity}";
            for (int j = 0; j < 7; j++)
            {
                DateTime dateTime = DateTime.Now;
                dateTime = dateTime.AddDays(j);
                res += $"\n\nDate: {dateTime.ToShortDateString()}\n\n";
                for (int i = 1; i < 24; i += 3)
                {
                    var temp = Math.Round(weather.hourly.temperature_2m[j * 7 + i]);
                    res += $"Time: {i}:00  <=>  {temp} °C\n";
                }
            }
            return res;
        }
        public async Task<string> ChangeCity(string city)
        {
            _uri = _configuration.GetSection("BaseAddress")[city];
            NowCity = city;
            return city;

        }

    }
}
