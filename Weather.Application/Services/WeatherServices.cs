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
            var weather = await GetWeatherIN7();

            //var weather = await _weatherServices.GetWeatherIN7();
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
            startIndex += takeCount;
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
            string res = $"Day: {maxNightTemperature} °C \nNight: {maxMorningTemperature} °C";
            return res;
        }
    }
}
