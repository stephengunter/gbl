using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using ApplicationCore.Helpers;
using Infrastructure.Entities;
using Infrastructure.Interfaces;

namespace ApplicationCore.Models
{
	public class City : BaseRecord
	{
		public string Code { get; set; }

		public string Title { get; set; }

		public ICollection<District> Districts { get; set; } = new List<District>();
		
	}

	public class District : BaseRecord
	{
		public string Zip { get; set; }

		public string Title { get; set; }

		public int CityId { get; set; }

		public City City { get; set; }


		public string FullName => City != null ? $"{City.Title}{Title}" : "";

	}
}
