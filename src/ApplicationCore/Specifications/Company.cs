using ApplicationCore.Helpers;
using ApplicationCore.Models;
using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationCore.Specifications
{

	public class CompanyFilterSpecification : BaseSpecification<Company>
	{
		public CompanyFilterSpecification() : base(item => !item.Removed)
		{
			AddInclude(item => item.District.City);
		}

		public CompanyFilterSpecification(string userId) : base(item => !item.Removed && item.UserId == userId)
		{
			AddInclude(item => item.District.City);
		}

		public CompanyFilterSpecification(int id) : base(item => !item.Removed && item.Id == id)
		{
			AddInclude(item => item.District.City);
		}
	}
}
