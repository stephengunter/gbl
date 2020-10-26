using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ApplicationCore.Helpers;
using Infrastructure.Views;

namespace ApplicationCore.Views
{
    public class CategoryViewModel : BaseCategoryView
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public string View { get; set; }

        public string Icon { get; set; }

        public ICollection<CategoryViewModel> SubItems { get; set; }

    }


}
