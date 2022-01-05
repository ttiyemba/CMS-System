using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Persistence.Models
{
	/// <summary>
	///  Model for the NAvigation Platform table
	/// </summary>
	public class NavigationPlatform
    {
		/// <summary>
		/// Primary key
		/// </summary>
		[Key]
		public int Id { get; set; }

		public string Identifier { get; set; }
		public float Longitude { get; set; }
		public float Latitude { get; set; }
		public int Elevation { get; set; }
		public float Bearing { get; set; }
		public float Speed { get; set; }
	}
}
