using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Views;
using ApplicationCore.Models;
using ApplicationCore.Paging;
using ApplicationCore.Helpers;
using System.Threading.Tasks;
using System.Linq;
using Infrastructure.Views;
using AutoMapper;

namespace ApplicationCore.ViewServices
{
	public static class CategoriesViewService
	{
		public static CategoryViewModel MapViewModel(this Category category, IMapper mapper)
		{ 
			var model = mapper.Map<CategoryViewModel>(category);
			if(category.SubItems.HasItems()) model.SubItems = category.SubItems.Select(item => MapViewModel(item, mapper)).ToList();

			return model;
		}

		public static Category MapEntity(this CategoryViewModel model, IMapper mapper)
			=> mapper.Map<CategoryViewModel, Category>(model);

		public static List<CategoryViewModel> MapViewModelList(this IEnumerable<Category> categories, IMapper mapper)
			=> categories.Select(item => MapViewModel(item, mapper)).ToList();

		public static IEnumerable<Category> GetOrdered(this IEnumerable<Category> categories)
			=> categories.OrderBy(item => item.Order);


	}
}
