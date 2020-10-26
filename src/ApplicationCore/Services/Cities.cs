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
    public interface ICitiesService
    {
        Task<IEnumerable<City>> FetchAsync();
    }

    public class CitiesService : ICitiesService
    {
        private readonly IDefaultRepository<City> _cityRepository;

        public CitiesService(IDefaultRepository<City> cityRepository)
        {
            this._cityRepository = cityRepository;
        }

        public async Task<IEnumerable<City>> FetchAsync()
            => await _cityRepository.ListAsync(new CityFilterSpecification());
    }
}
