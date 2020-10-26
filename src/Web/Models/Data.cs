using ApplicationCore.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class DataViewModel
    {
        public ICollection<CityViewModel> Cities { get; set; }

        public ICollection<CategoryViewModel> Categories { get; set; }
    }
}
