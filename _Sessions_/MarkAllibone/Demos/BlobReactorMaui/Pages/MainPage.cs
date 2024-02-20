using BlobReactorMaui.Core;
using MauiReactor;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobReactorMaui.Pages
{
    internal class MainPageState
    {
        public bool IsLoading { get; internal set; }
        public string Postalcode { get; internal set; } = string.Empty;
        public WeatherForecast WeatherReport { get; internal set; } = new();
    }

    internal class MainPage : Component<MainPageState>
    {
        private IWeatherService _weatherService;
        private IDisposable _currentRequest;

        public MainPage() : this(null) { }
        public MainPage(IWeatherService? weatherService = null)
        {
            _weatherService = weatherService ?? Services.GetRequiredService<IWeatherService>();
        }

        public override VisualNode Render()
        {
            return new ContentPage
        {
            new ScrollView
            {
                new VerticalStackLayout
                {
                    new Label("Hello, World!")
                        .FontSize(32)
                        .HCenter(),

                    new Entry()
                        .OnAfterTextChanged(t => SetState(s => s.Postalcode = t))
                        .FontSize(18)
                        .HCenter(),

                    new Label("Temperature: " + State.WeatherReport.TemperatureC + "° C")
                        .IsVisible(State.WeatherReport.Date != default)
                        .HCenter(),
                    new Label("Humidity: " + State.WeatherReport.Humidity + "%")
                        .IsVisible(State.WeatherReport.Date != default)
                        .HCenter(),
                    new Label("Windspeed: " + State.WeatherReport.Windspeed + " kph")
                        .IsVisible(State.WeatherReport.Date != default)
                        .HCenter(),
                    new Label("Summary: " + State.WeatherReport.Summary)
                        .IsVisible(State.WeatherReport.Date != default)
                        .HCenter(),

                    new Button("Get Weather")
                        .OnClicked(GetWeather)
                        .IsEnabled(!State.IsLoading)
                        .HCenter(),
                    new Button("Cancel")
                        .OnClicked(CancelWeatherRequest)
                        .IsVisible(State.IsLoading)
                        .IsEnabled(State.IsLoading)
                        .HCenter()
                }
                .VCenter()
                .Spacing(25)
                .Padding(30, 0)
            }
        };
        }

        private void CancelWeatherRequest()
        {
            _currentRequest.Dispose();
            SetState(s => s.IsLoading = false);
        }

        private void GetWeather()
        {
            SetState(s => s.IsLoading = true);
            _currentRequest = _weatherService.GetWeatherForecastNoCache(Convert.ToInt32(State.Postalcode))
                .Subscribe(t =>
                {
                    SetState(s => s.WeatherReport = t);
                }, () => SetState(s => s.IsLoading = false));
        }

        protected override void OnMounted()
        {
            base.OnMounted();
            //_weatherService = Services.GetRequiredService<IWeatherService>();
        }
    }
}