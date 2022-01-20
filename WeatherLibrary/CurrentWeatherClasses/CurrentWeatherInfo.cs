using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLibrary
{
    public class CurrentWeatherInfo
    {
        public Cordionate Coord { get; set; }

        public List<Weather> Weather { get; set; }

        public MainWeather Main { get; set; }

        public WindWeather Wind { get; set; }
        public Sys Sys { get; set; }

        public int Visibility { get; set; }

        public string Name { get; set; }
    }
}
