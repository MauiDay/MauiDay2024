namespace BlobReactorMaui.Core;

public class WeatherForecast
{
    public DateTime Date { get; set; }
    public float TemperatureC { get; set; }
    public float TemperatureF { get; set; }
    public int Windspeed { get; set; } // kph
    public int Humidity { get; set; } // %
    public string Summary { get; set; } = string.Empty;
    public int PostalCode { get; set; }
}