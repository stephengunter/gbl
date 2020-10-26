using ApplicationCore.Models;
using ApplicationCore.Views;
using AutoMapper;
using ApplicationCore.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.DtoMapper
{
	public class CityMappingProfile : Profile
	{
		public CityMappingProfile()
		{
			CreateMap<City, CityViewModel>();

			CreateMap<CityViewModel, City>();
		}
	}
}
