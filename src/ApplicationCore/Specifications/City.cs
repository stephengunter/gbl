using ApplicationCore.Helpers;
using ApplicationCore.Models;
using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationCore.Specifications
{

	public class CityFilterSpecification : BaseSpecification<City>
	{
		public CityFilterSpecification() : base(item => !item.Removed)
		{
			AddInclude(item => item.Districts);
		}
	}
}
