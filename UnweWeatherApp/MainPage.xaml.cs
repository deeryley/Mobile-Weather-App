using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace UnweWeatherApp
{
    public partial class MainPage : ContentPage
    {
        WeatherService _WeatherService;
        public MainPage()
        {
            InitializeComponent();
            GetWeatherWithGeoLoaction();
            _WeatherService = new WeatherService(); 
        }


        string GenerateRequestUriGeo(string endpoint, double lati, double longt)
        {
            string requestUri = endpoint;
            requestUri += $"?lat={lati}";
            requestUri += $"&lon={longt}";
            requestUri += "&units=imperial";
            requestUri += $"&APPID={Constants.OpenWeatherMapAPIKey}";
            return requestUri;
        }
        string GenerateRequestUri(string endpoint)
        {
            string requestUri = endpoint;
            requestUri += $"?q={_cityEntry.Text}";
            requestUri += "&units=metric"; // or units = metric
            requestUri += "&cnt=2";
            requestUri += $"&APPID={Constants.OpenWeatherMapAPIKey}";
            return requestUri;
        }

        private WeatherService Get_WeatherService()
        {
            return _WeatherService;
        }

        private async void OnGetWeatherButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_cityEntry.Text))
            {
                WeatherData weatherData = await _WeatherService.GetWeatherData(GenerateRequestUri(Constants.OpenWeatherMapEndpoint));
                BindingContext = weatherData;
            }

        }
        private async void GetWeatherWithGeoLoaction ()
        {
            var location = await Geolocation.GetLocationAsync();
            if (location !=  null)
            {


                WeatherData weatherData = await _WeatherService.GetWeatherData(GenerateRequestUriGeo(Constants.OpenWeatherMapEndpoint, location.Latitude, location.Longitude)); 
                BindingContext = weatherData;
            }
            
        }
         

    }
}

