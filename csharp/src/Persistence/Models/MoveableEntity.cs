using src.Persistence.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.Persistence.Models

{
	/// <summary>
	/// Model for the MoveableEntities table
	/// </summary>
	public class MoveableEntity 

	{
		/// <summary>
		/// Primary key
		/// </summary>
		[Key]
		public int Id { get; set; }

		public float Bearing { get; set; }
		public float Speed { get; set; }
		public float Heading { get; set; }

		/// <summary>
		/// Foreign Key
		/// </summary>
		public int PlottableEntityID { get; set; }

		/// <summary>
		/// reference navigation property
		/// </summary>
		[ForeignKey("PlottableEntityID")]
		public PlottableEntity PlottableEntity { get; set; }
	}
}
