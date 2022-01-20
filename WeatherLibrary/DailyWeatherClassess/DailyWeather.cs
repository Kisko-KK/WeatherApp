using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherLibrary.DailyWeatherClassess;

namespace WeatherLibrary
{
    public class DailyWeather
    {
        public DTemperature Temp { get; set; }
        public List<Weather> Weather { get; set; }
        public long Dt { get; set; }
    }
}
