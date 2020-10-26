using ApplicationCore.Models;
using ApplicationCore.Views;
using AutoMapper;
using ApplicationCore.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.DtoMapper
{
	public class DistrictMappingProfile : Profile
	{
		public DistrictMappingProfile()
		{
			CreateMap<District, DistrictViewModel>();

			CreateMap<DistrictViewModel, District>();
		}
	}
}
