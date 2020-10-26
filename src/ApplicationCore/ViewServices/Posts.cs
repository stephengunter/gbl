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
	public static class PostsViewService
	{
		public static PostViewModel MapViewModel(this Post post, IMapper mapper, ICollection<UploadFile> attachmentsList = null)
		{
			if (attachmentsList.HasItems()) post.LoadAttachments(attachmentsList);

			var model = mapper.Map<PostViewModel>(post);

			return model;
		}

		public static Post MapEntity(this PostViewModel model, IMapper mapper, string userId)
		{
			var entity = mapper.Map<PostViewModel, Post>(model);

			if (model.Id == 0) entity.SetCreated(userId);
			entity.SetUpdated(userId);

			return entity;
		}

		public static List<PostViewModel> MapViewModelList(this IEnumerable<Post> posts, IMapper mapper)
			=> posts.Select(item => MapViewModel(item, mapper)).ToList();


	}
}
