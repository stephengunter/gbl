using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Views;
using ApplicationCore.Helpers;
using ApplicationCore.Consts;
using AutoMapper;
using ApplicationCore.Settings;
using Microsoft.Extensions.Options;
using Web.Controllers;
using ApplicationCore.ViewServices;
using Microsoft.Extensions.Caching.Memory;

namespace Web.Controllers.Admin
{
    public class DataController : BaseAdminController
    {
        private readonly AdminSettings _adminSettings;
		private IMemoryCache _cache;

		

		public DataController(IOptions<AdminSettings> adminSettings, IMemoryCache cache)
		{
			_adminSettings = adminSettings.Value;
			_cache = cache;
		}

		[HttpPost("cities")]
		public ActionResult ClearCities(AdminRequest model)
		{
			ValidateRequest(model, _adminSettings);
			if (!ModelState.IsValid) return BadRequest(ModelState);

			_cache.Remove(CacheKeys.Cities);
			return Ok();
		}

		[HttpPost("categories")]
		public ActionResult ClearCategories(AdminRequest model)
		{
			ValidateRequest(model, _adminSettings);
			if (!ModelState.IsValid) return BadRequest(ModelState);

			_cache.Remove(CacheKeys.Categories);
			return Ok();
		}
	}
}
