using ApplicationCore.Helpers;
using ApplicationCore.Services;
using ApplicationCore.Views;
using ApplicationCore.ViewServices;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers.Api
{
	[Authorize]
	public class PostsController : BaseApiController
	{
		private readonly IPostsService _postsService;
		private readonly ICompaniesService _companiesService;
		private readonly IMapper _mapper;

		public PostsController(IPostsService postsService, ICompaniesService companiesService, IMapper mapper)
		{
			_postsService = postsService;
			_companiesService = companiesService;
			_mapper = mapper;
		}

		[AllowAnonymous]
		[HttpGet("")]
		public async Task<ActionResult> Index()
		{
			var posts = await _postsService.FetchByUserAsync(CurrentUserId);
			if (posts.IsNullOrEmpty()) return Ok(new List<PostViewModel>());

			return Ok(posts.MapViewModelList(_mapper));
		}

		[HttpGet("create")]
		public async Task<ActionResult> Create()
		{
			var model = new PostEditForm() { Post = new PostViewModel() };

			var companies = await _companiesService.FetchByUserAsync(CurrentUserId);
			if (companies.HasItems()) model.Companies = companies.MapViewModelList(_mapper);

			return Ok(model);
		}

		[HttpPost("")]
		public async Task<ActionResult> Store([FromBody] PostViewModel model)
		{
			ValidateRequest(model);
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var post = model.MapEntity(_mapper, CurrentUserId);

			post = await _postsService.CreateAsync(post);

			return Ok(post.Id);
		}

		void ValidateRequest(PostViewModel model)
		{
			if (String.IsNullOrEmpty(model.Title)) ModelState.AddModelError("title", "必須填寫標題");

		}

	}

}