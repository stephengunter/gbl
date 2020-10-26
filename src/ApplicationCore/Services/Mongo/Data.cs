using ApplicationCore.Settings;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Options;
using ApplicationCore.Models.Mongo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Views;
using ApplicationCore.Helpers;
using Newtonsoft.Json;
using System.Linq;

namespace ApplicationCore.Services
{
    public interface IDataService
    {
        IEnumerable<CityViewModel> FetchCities();
        void SaveCities(List<CityViewModel> models);

        IEnumerable<CategoryViewModel> FetchCategories();
        void SaveCategories(List<CategoryViewModel> models);
    }

    public class DataService : IDataService
    {
        private readonly IMongoRepository<Cities> _citiesRepository;
        private readonly IMongoRepository<Categories> _categoriesRepository;

        public DataService(IMongoRepository<Cities> citiesRepository, IMongoRepository<Categories> categoriesRepository)
        {
            _citiesRepository = citiesRepository;
            _categoriesRepository = categoriesRepository;
        }

        public IEnumerable<CityViewModel> FetchCities()
        {
            var doc = _citiesRepository.FindOne(x => true);
            if (doc == null) return null;

            return JsonConvert.DeserializeObject<IEnumerable<CityViewModel>>(doc.Content);
        }
        public void SaveCities(List<CityViewModel> models)
        {
            _citiesRepository.DeleteMany(x => true);

            string content = JsonConvert.SerializeObject(models);
            _citiesRepository.InsertOne(new Cities { Content = content });
        }


        public IEnumerable<CategoryViewModel> FetchCategories()
        {
            var doc = _categoriesRepository.FindOne(x => true);
            if (doc == null) return null;

            return JsonConvert.DeserializeObject<IEnumerable<CategoryViewModel>>(doc.Content);
        }
        public void SaveCategories(List<CategoryViewModel> models)
        {
            _categoriesRepository.DeleteMany(x => true);

            string content = JsonConvert.SerializeObject(models);
            _categoriesRepository.InsertOne(new Categories { Content = content });
        }


    }
}
