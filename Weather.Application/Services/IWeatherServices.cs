using Weather.Domain.Entities;

namespace Weather.Application.Services
{
    public interface IWeatherServices
    {
        Task<WeatherForecast> GetWeatherIN7();
        Task<string> GetWeatherToday();

    }
}