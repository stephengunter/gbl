using ApplicationCore.Consts;
using ApplicationCore.Helpers;
using ApplicationCore.Migrations;
using ApplicationCore.Services;
using ApplicationCore.Settings;
using ApplicationCore.Views;
using ApplicationCore.ViewServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers.Api
{
	public class ATestsController : BaseApiController
	{
		private readonly AdminSettings _adminSettings;
		private readonly ICompaniesService _companiesService;
		private readonly IMapper _mapper;

		public ATestsController(IOptions<AdminSettings> adminSettings, 
			ICompaniesService companiesService, IMapper mapper)
		{
			_adminSettings = adminSettings.Value;
			_companiesService = companiesService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult>  Index()
		{
			string userId = _adminSettings.Id;
			var companies = await _companiesService.FetchByUserAsync(userId);
			if (companies.IsNullOrEmpty()) return NotFound();

			return Ok(companies.MapViewModelList(_mapper));
		}

		async Task<ActionResult> SaveCompany()
		{
			string userId = _adminSettings.Id;
			var model = new CompanyViewModel
			{
				Title = "海瑞商行",
				Phone = "0936060049",
				Address = new AddressViewModel
				{
					District = new DistrictViewModel
					{
						Id = 40,
						Zip = "234",
						Title = "永和區",
					},
					Street = "中山路223號"
				}
			};

			ValidateRequest(model);
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var company = model.MapEntity(_mapper, userId);

			company = await _companiesService.CreateAsync(company);

			return Ok(company.Id);
		}

		void ValidateRequest(CompanyViewModel model)
		{
			//if (model.Public)
			//{
			//	if (String.IsNullOrEmpty(model.Title)) ModelState.AddModelError("title", "必須填寫標題");
			//}

		}

	}

}