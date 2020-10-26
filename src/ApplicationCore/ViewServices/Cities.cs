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
	public static class CitiesViewService
	{
		public static CityViewModel MapViewModel(this City city, IMapper mapper)
			=> mapper.Map<CityViewModel>(city);

		public static City MapEntity(this CityViewModel model, IMapper mapper)
			=> mapper.Map<CityViewModel, City>(model);

		public static List<CityViewModel> MapViewModelList(this IEnumerable<City> cities, IMapper mapper)
			=> cities.Select(item => MapViewModel(item, mapper)).ToList();


		public static DistrictViewModel MapViewModel(this District district, IMapper mapper)
		{
			var model = mapper.Map<DistrictViewModel>(district);
			
			return model;
		}

		public static District MapEntity(this DistrictViewModel model, IMapper mapper)
			=> mapper.Map<DistrictViewModel, District>(model);

		public static List<DistrictViewModel> MapViewModelList(this IEnumerable<District> districts, IMapper mapper)
			=> districts.Select(item => MapViewModel(item, mapper)).ToList();

	}
}
