using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.DataAccess;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Identity;
using ApplicationCore.Helpers;
using ApplicationCore.Exceptions;
using ApplicationCore.Specifications;

namespace ApplicationCore.Services
{
    public interface IPostsService
    {
        Task<IEnumerable<Post>> FetchAsync();
        Task<IEnumerable<Post>> FetchByUserAsync(string userId);
        Task<Post> CreateAsync(Post post);
    }

    public class PostsService : IPostsService
    {
        private readonly IDefaultRepository<Post> _postRepository;

        public PostsService(IDefaultRepository<Post> postRepository)
        {
            this._postRepository = postRepository;
        }

        public async Task<IEnumerable<Post>> FetchAsync()
            => await _postRepository.ListAsync(new PostFilterSpecification());

        public async Task<IEnumerable<Post>> FetchByUserAsync(string userId)
            => await _postRepository.ListAsync(new PostFilterSpecification(userId));

        public async Task<Post> CreateAsync(Post post) => await _postRepository.AddAsync(post);
    }
}
