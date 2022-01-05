using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Dto
{
    public class PlottableEntityWithDistanceDto
    {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public string Classification { get; set; }
        public string Subclassification { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public int Elevation { get; set; }
        public double Distance { get; set; }
        public ulong LastUpdate { get; set; }
        public bool Armed { get; set; }
        public string Hostility { get; set; }
    }
}
