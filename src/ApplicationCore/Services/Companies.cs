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
    public interface ICompaniesService
    {
        Task<IEnumerable<Company>> FetchByUserAsync(string userId);

        Task<Company> GetByIdAsync(int id);
        Company GetById(int id);

        Task<Company> CreateAsync(Company company);
        Task UpdateAsync(Company existingEntity, Company company);
        Task RemoveAsync(Company company);
        Task TopAsync(Company company);
    }

    public class CompaniesService : ICompaniesService
    {
        private readonly IDefaultRepository<Company> _companyRepository;

        public CompaniesService(IDefaultRepository<Company> companyRepository)
        {
            this._companyRepository = companyRepository;
        }


        public async Task<IEnumerable<Company>> FetchByUserAsync(string userId)
        { 
            var allItems = await _companyRepository.ListAsync(new CompanyFilterSpecification(userId));

            var items = allItems.Where(x => x.ParentId == 0).ToList();

            foreach (var item in items)
            {
                item.LoadSubItems(allItems);
            }

            return items;
        }
        
        public async Task<Company> CreateAsync(Company company) => await _companyRepository.AddAsync(company);

        public async Task<Company> GetByIdAsync(int id) => await _companyRepository.GetByIdAsync(id);

        public Company GetById(int id) 
            => _companyRepository.GetSingleBySpec(new CompanyFilterSpecification(id));

        public async Task UpdateAsync(Company existingEntity, Company company)
            => await _companyRepository.UpdateAsync(existingEntity, company);

        public async Task RemoveAsync(Company company)
        {
            company.Removed = true;
            await _companyRepository.UpdateAsync(company);
        }

        public async Task TopAsync(Company company)
        {
            var companies = await FetchByUserAsync(company.UserId);
            foreach (var item in companies) item.Top = (item.Id == company.Id);
            _companyRepository.UpdateRange(companies);
        }
    }
}
