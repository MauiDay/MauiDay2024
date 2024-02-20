using Akavache;
using System.Reactive.Linq;
using System.Text.Json;

namespace BlobReactorMaui.Core;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private const string BackendUrl = "https://mauireactivewebapi.azurewebsites.net/api/weatherforecast/forpostalcode/{0}";

    public WeatherService()
    {
        _httpClient = new HttpClient();
    }

    private IObservable<WeatherForecast> GetWeatherForecast(int postalCode)
    {
        var url = string.Format(BackendUrl, postalCode);
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        return
            Observable.StartAsync(() => _httpClient.GetStringAsync(url))
            .Select(forecast => JsonSerializer.Deserialize<WeatherForecast>(forecast, options)!);
    }
    public IObservable<WeatherForecast> GetWeatherForecastNoCache(int postalCode)
    {
        return GetWeatherForecast(postalCode);
    }

    public IObservable<WeatherForecast> GetWeatherForecastCache(int postalCode)
    {
        return

        // Expiration Cache (the classic)
        BlobCache.InMemory.GetOrFetchObject(
            $"forecast{postalCode}",
            () => GetWeatherForecast(postalCode),
            DateTimeOffset.Now.AddSeconds(5))!;

        // Get and fetch latest
        // BlobCache.LocalMachine.GetAndFetchLatest($"forecast{postalCode}",
        //     () => GetWeatherForecast(postalCode))!;
    }
}
