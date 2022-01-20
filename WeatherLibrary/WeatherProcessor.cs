using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLibrary
{
    public class WeatherProcessor
    {
        private static string APIkey = "e72a225803fca252b1c162bece47e4c3";
        public static async Task<CurrentWeatherInfo> LoadWeatherInfo(string cityName)
        {
            string url = $"http://api.openweathermap.org/data/2.5/weather?q={cityName}&units=metric&appid={APIkey}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    CurrentWeatherInfo weather = await response.Content.ReadAsAsync<CurrentWeatherInfo>();
                    return weather;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


        public static async Task<DailyWeatherInfo> LoadDailyWeatherInfo(double lat, double lon)
        {
            string url = $"https://api.openweathermap.org/data/2.5/onecall?lat={lat}&lon={lon}&units=metric&exclude=minutely,current,hourly,alerts&appid={APIkey}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    DailyWeatherInfo weather = await response.Content.ReadAsAsync<DailyWeatherInfo>();

                    return weather;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


    }
}
