using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ApplicationCore.Helpers;
using Infrastructure.Views;

namespace ApplicationCore.Views
{
    public class CompanyViewModel : BaseCategoryView
    {
        public string UserId { get; set; }

        public bool Top { get; set; }
       
        public string Phone { get; set; }

        public AddressViewModel Address { get; set; } = new AddressViewModel();

        public ICollection<CompanyViewModel> SubItems { get; set; }
    }

}
