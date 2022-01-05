using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#nullable enable
namespace src.Dto
{
    public class WeatherDto
    {
        public float? Temp { get; set; }
        public int? Visibility { get; set; }
        public float? Pressure { get; set; }
        public float? Humidity { get; set; }
        public float? WindSpeed { get; set; }
        public int? WindDegree { get; set; }
        public int? Cloud { get; set; }

    }
}
