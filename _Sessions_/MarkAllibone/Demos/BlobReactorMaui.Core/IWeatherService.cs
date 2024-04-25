namespace BlobReactorMaui.Core;

public interface IWeatherService
{
    IObservable<WeatherForecast> GetWeatherForecastCache(int postalCode);
    IObservable<WeatherForecast> GetWeatherForecastNoCache(int postalCode);
}