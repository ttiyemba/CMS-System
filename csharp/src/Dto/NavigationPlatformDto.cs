using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Dto
{
    public class NavigationPlatformDto
	{
		public string Identifier { get; set; }
		public float Longitude { get; set; }
		public float Latitude { get; set; }
		public int Elevation { get; set; }
		public float Bearing { get; set; }
		public float Speed { get; set; }
	}
}
