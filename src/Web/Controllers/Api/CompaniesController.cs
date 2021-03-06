﻿using ApplicationCore.Helpers;
using ApplicationCore.Models;
using ApplicationCore.Services;
using ApplicationCore.Views;
using ApplicationCore.ViewServices;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers.Api
{
	[Authorize]
	public class CompaniesController : BaseApiController
	{
		private readonly ICompaniesService _companiesService;
		private readonly IMapper _mapper;

		public CompaniesController(ICompaniesService companiesService, IMapper mapper)
		{
			_companiesService = companiesService;
			_mapper = mapper;
		}

		[HttpGet("")]
		public async Task<ActionResult> Index()
		{
			var companies = await _companiesService.FetchByUserAsync(CurrentUserId);
			if (companies.IsNullOrEmpty()) return Ok(new List<CompanyViewModel>());

			var rootCompanies = companies.Where(x => x.IsRootItem);
			var subCompanies = companies.Where(x => !x.IsRootItem);

			foreach (var rootItem in rootCompanies)
			{
				rootItem.LoadSubItems(subCompanies);
			}


			rootCompanies = rootCompanies.GetOrdered();

			return Ok(rootCompanies.MapViewModelList(_mapper));
		}

		[HttpGet("create")]
		public ActionResult Create() => Ok(new CompanyViewModel());

		[HttpPost("")]
		public async Task<ActionResult> Store([FromBody] CompanyViewModel model)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var company = model.MapEntity(_mapper, CurrentUserId);


			company = await _companiesService.CreateAsync(company);

			return Ok(company.Id);
		}

		[HttpGet("edit/{id}")]
		public ActionResult Edit(int id)
		{
			var company = _companiesService.GetById(id);
			if (company == null) return NotFound();

			return Ok(company.MapViewModel(_mapper));
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(int id, [FromBody] CompanyViewModel model)
		{
			var existingEntity = await _companiesService.GetByIdAsync(id);
			if (existingEntity == null) return NotFound();

			if (!ModelState.IsValid) return BadRequest(ModelState);

			var company = model.MapEntity(_mapper, CurrentUserId);

			await _companiesService.UpdateAsync(existingEntity, company);

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var company = await _companiesService.GetByIdAsync(id);
			if (company == null) return NotFound();

			company.SetUpdated(CurrentUserId);
			await _companiesService.RemoveAsync(company);

			return Ok();
		}

		[HttpPut("top/{id}")]
		public async Task<ActionResult> Top(int id)
		{
			var company = await _companiesService.GetByIdAsync(id);
			if (company == null) return NotFound();

			company.SetUpdated(CurrentUserId);
			await _companiesService.TopAsync(company);

			return Ok();
		}

		[HttpPost("orders")]
		public async Task<ActionResult> Orders([FromBody] List<SubOrderRequest> models)
		{
			var companies = await _companiesService.FetchByUserAsync(CurrentUserId);

			var subCompanies = new List<Company>();
			foreach (var model in models)
			{
				for (int i = 0; i < model.Orders.Count; i++)
				{
					var subCompany = companies.FirstOrDefault(x => x.Id == model.Orders[i]);
					subCompany.Order = i;

					subCompany.SetUpdated(CurrentUserId);
					subCompanies.Add(subCompany);
				}
				
			}

			_companiesService.UpdateMany(subCompanies);

			return Ok();
		}


	}

}