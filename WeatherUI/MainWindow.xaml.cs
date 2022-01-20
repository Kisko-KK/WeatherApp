using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeatherLibrary;

namespace WeatherUI
{
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ApiHelper.InitializeClient();
            SetDefaultCurrentDailyInfo();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string cityName = InputCityName.Text;
            InputCityName.Text = "";
            ErrorMessage.Visibility = Visibility.Hidden;

            try
            {
                var weatherInfo = await WeatherProcessor.LoadWeatherInfo(cityName);
                double lon = weatherInfo.Coord.Lon;
                double lat = weatherInfo.Coord.Lat;
                var forecastInfo = await WeatherProcessor.LoadDailyWeatherInfo(lat, lon);

                SetCurrentWeatherInfo(cityName,weatherInfo);
                SetDailyForecasts(forecastInfo);
            }
            catch (System.Net.Http.HttpRequestException)
            {
                ErrorMessage.Content = "Can't connect to server.";
                ErrorMessage.Visibility = Visibility.Visible;
            }
            catch(Exception)
            {
                ErrorMessage.Content = "Please enter valid city name.";
                ErrorMessage.Visibility = Visibility.Visible;

            }
        }

        private void SetCurrentWeatherInfo(string cityName, CurrentWeatherInfo weatherInfo) {

            string currentTime = DateTime.Now.ToShortTimeString();

            LabelTime.Content = $"{cityName.ToUpper()} {currentTime}";
            LabelFeelsLike.Content = $"FEELS LIKE: {Math.Round(weatherInfo.Main.Feels_like,1)}°C";
            LabelTemperature.Content = $"{Math.Round(weatherInfo.Main.Temp,1)}°C";
            LabelHumidity.Content = $"{weatherInfo.Main.Humidity}%";
            LabelPresure.Content = $"{weatherInfo.Main.Pressure} mb";
            LabelVisibility.Content = $"{weatherInfo.Visibility / 1000} km";
            LabelWind.Content = $"{Math.Round(weatherInfo.Wind.Speed,0)} km/h";

            string iconLink = $"https://openweathermap.org/img/w/{weatherInfo.Weather[0].Icon}.png";
            IconCurrentWeather.Source = new BitmapImage(new Uri(iconLink));
        }

     
        private void SetDailyForecasts(DailyWeatherInfo dailyWeather) {

            DayWeatherIteam[] dayWeatherIteams = new DayWeatherIteam[4];
            panel.Children.Clear();

            for(int i = 0; i < 4; i++)
            {
                dayWeatherIteams[i] = new DayWeatherIteam();
                DateTime date = ConvertDateTime(dailyWeather.Daily[i].Dt);
                dayWeatherIteams[i].date.Content = $"{date.DayOfWeek}, {date.Day}";
                
                string iconLink = $"https://openweathermap.org/img/w/{dailyWeather.Daily[i].Weather[0].Icon}.png";
                dayWeatherIteams[i].icon.Source = new BitmapImage(new Uri(iconLink));

                dayWeatherIteams[i].minTemp.Content = $"{Math.Round(dailyWeather.Daily[i].Temp.Min,1)}°C";
                dayWeatherIteams[i].maxTemp.Content = $"{Math.Round(dailyWeather.Daily[i].Temp.Max, 1)}°C";


                panel.Children.Add(dayWeatherIteams[i]);
            }
        }

        private void SetDefaultCurrentDailyInfo()
        {
            DayWeatherIteam[] dayWeatherIteams = new DayWeatherIteam[4];
            panel.Children.Clear();

            for (int i = 0; i < 4; i++)
            {                
                dayWeatherIteams[i] = new DayWeatherIteam();

                dayWeatherIteams[i].date.Content = "DayOfWeek";
                string iconLink = $"https://openweathermap.org/img/w/01d.png";
                dayWeatherIteams[i].icon.Source = new BitmapImage(new Uri(iconLink));

                dayWeatherIteams[i].minTemp.Content = "0°C";
                dayWeatherIteams[i].maxTemp.Content = "0°C";

                panel.Children.Add(dayWeatherIteams[i]);
            }
        }


        private static DateTime ConvertDateTime(long seconds)
        {
            DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            date = date.AddSeconds(seconds).ToLocalTime();

            return date;
        }

    }
}
