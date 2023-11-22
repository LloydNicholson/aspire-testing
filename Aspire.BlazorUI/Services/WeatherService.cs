namespace Aspire.BlazorUI.Services;

using Core;

public class WeatherService(HttpClient httpClient)
{
    public Task<IList<WeatherForecast>?> GetWeatherForecasts()
    {
        return httpClient.GetFromJsonAsync<IList<WeatherForecast>>("weatherforecast");
    }
}
