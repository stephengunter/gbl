using ApplicationCore.Consts;
using ApplicationCore.Helpers;
using ApplicationCore.Services;
using ApplicationCore.Views;
using ApplicationCore.ViewServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers.Api
{
	public class DataController : BaseApiController
	{
		private readonly ICitiesService _citiesService;
		private readonly ICategoriesService _categoriesService;
		private readonly IMapper _mapper;

		private IMemoryCache _cache;

		public DataController(ICitiesService citiesService, ICategoriesService categoriesService,
			IMemoryCache cache, IMapper mapper)
		{

			_citiesService = citiesService;
			_categoriesService = categoriesService;

			_cache = cache;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult> Index()
		{
			var cities = await FetchCitiesAsync();
			var categories = await FetchCategoriesAsync();

			var model = new DataViewModel
			{
				Cities = cities.ToList(),
				Categories = categories.ToList()
			};

			return Ok(model);

		}

		async Task<IEnumerable<CityViewModel>> FetchCitiesAsync()
		{
			string content;
			if (_cache.TryGetValue(CacheKeys.Cities, out content)) return JsonConvert.DeserializeObject<IEnumerable<CityViewModel>>(content);

			var cities = await _citiesService.FetchAsync();
			cities = cities.Where(x => x.Active).ToList();

			foreach (var city in cities)
			{
				city.Districts = city.Districts.OrderBy(x => x.Zip.ToInt()).ToList();

				foreach (var district in city.Districts) district.City = city;
			}

			var models = cities.MapViewModelList(_mapper);

			_cache.Set(CacheKeys.Cities, JsonConvert.SerializeObject(models),
				new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(7)));

			return models;
		}

		async Task<IEnumerable<CategoryViewModel>> FetchCategoriesAsync()
		{
			string content;
			if (_cache.TryGetValue(CacheKeys.Categories, out content)) return JsonConvert.DeserializeObject<IEnumerable<CategoryViewModel>>(content);

			var categories = await _categoriesService.FetchAsync();
			categories = categories.Where(x => x.Active).ToList();

			var models = categories.MapViewModelList(_mapper);

			_cache.Set(CacheKeys.Categories, JsonConvert.SerializeObject(models),
				new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(7)));

			return models;
		}

	}

}