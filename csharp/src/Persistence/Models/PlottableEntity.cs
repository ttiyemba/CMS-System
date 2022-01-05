using System;
using System.ComponentModel.DataAnnotations;
using src.Persistence.Models;


namespace src.Persistence.Models

{
	/// <summary>
	///  Model for the PlottableEntities table
	/// </summary>
	public class PlottableEntity
	{
		/// <summary>
		/// Primary key
		/// </summary>
		[Key]
		public int Id { get; set; }

		public string Identifier { get; set; }
		public string Classification { get; set; }
		public string Subclassification { get; set; }
		public float Longitude { get; set; }
		public float Latitude { get; set; }
		public int Elevation { get; set; }
		public ulong LastUpdate { get; set; }
		public bool Armed { get; set; }
		public string Hostility { get; set; }

		public MoveableEntity MoveableEntity { get; set; } = new MoveableEntity();
    }
}