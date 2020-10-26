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
    public interface ICategoriesService
    {
        Task<IEnumerable<Category>> FetchAsync();
    }

    public class CategoriesService : ICategoriesService
    {
        private readonly IDefaultRepository<Category> _categoryRepository;

        public CategoriesService(IDefaultRepository<Category> categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> FetchAsync()
        {
            var allItems = await _categoryRepository.ListAsync(new CategoryFilterSpecification());

            var items = allItems.Where(x => x.ParentId == 0).ToList();

            foreach (var item in items)
            {
                item.LoadSubItems(allItems);
            }

            return items;

        }
    }
}
