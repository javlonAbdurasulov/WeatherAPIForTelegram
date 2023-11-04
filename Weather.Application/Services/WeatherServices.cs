using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Weather.Domain.Entities;

namespace Weather.Application.Services
{
    public class WeatherServices : IWeatherServices
    {
        private readonly HttpClient _httpClient;
        private readonly string _uri;
        public WeatherServices(HttpClient httpClient,IConfiguration configuration)
        {
            _httpClient = httpClient;
            _uri = configuration.GetSection("BaseAddress")["WeatherAPI"];
            _httpClient.BaseAddress = new Uri($"{_uri}");
        }
        public async Task<WeatherForecast> GetWeatherIN7()
        {

            var response = await _httpClient.GetAsync("");
            var responseModel = await response.Content.ReadFromJsonAsync<WeatherForecast>();

            return responseModel;
        }

        public async Task<string> GetWeatherToday()
        {
            WeatherForecast weather = await GetWeatherIN7();
            string res = "City:  Tashkent\n";
            for (int i = 1; i < 24; i+=3)
            {
                var temp = Math.Round(weather.hourly.temperature_2m[i]);
                res += $"Time: {i+1}:00  <=>  {temp} °C\n";
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
    }
}
