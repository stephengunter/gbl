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
	public static class CompaniesViewService
	{
		public static CompanyViewModel MapViewModel(this Company company, IMapper mapper)
		{
			var model = mapper.Map<CompanyViewModel>(company);

			model.Address = new AddressViewModel
			{
				District = company.District.MapViewModel(mapper),
				Street = company.Street
			};

			if (company.SubItems.HasItems()) model.SubItems = company.SubItems.Select(item => MapViewModel(item, mapper)).ToList();


			return model;
		}

		public static Company MapEntity(this CompanyViewModel model, IMapper mapper, string userId)
		{
			var entity = mapper.Map<CompanyViewModel, Company>(model);

			entity.Phone = entity.Phone.RemoveSciptAndHtmlTags();
			entity.Phone = entity.Phone.ReplaceNewLine();

			entity.UserId = userId;
			entity.DistrictId = model.Address.District.Id;
			entity.Street = model.Address.Street;

			if (model.Id == 0) entity.SetCreated(userId);
			else entity.SetUpdated(userId);

			return entity;
		}
		public static List<CompanyViewModel> MapViewModelList(this IEnumerable<Company> companies, IMapper mapper)
			=> companies.Select(item => MapViewModel(item, mapper)).ToList();


		public static IEnumerable<Company> GetOrdered(this IEnumerable<Company> companies)
			=> companies.OrderByDescending(item => item.Top).ThenBy(item => item.Order);

	}
}
