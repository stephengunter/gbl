using ApplicationCore.Helpers;
using ApplicationCore.Models;
using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationCore.Specifications
{

	public class CategoryFilterSpecification : BaseSpecification<Category>
	{
		public CategoryFilterSpecification() : base(item => !item.Removed)
		{
			
		}
	}
}
