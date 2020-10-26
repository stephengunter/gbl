using ApplicationCore.Helpers;
using ApplicationCore.Models;
using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationCore.Specifications
{

	public class PostFilterSpecification : BaseSpecification<Post>
	{
		public PostFilterSpecification() : base(item => !item.Removed)
		{
			
		}

		public PostFilterSpecification(string userId) : base(item => !item.Removed && item.UserId == userId)
		{
			
		}

		public PostFilterSpecification(int id) : base(item => !item.Removed && item.Id == id)
		{
			
		}
	}
}
