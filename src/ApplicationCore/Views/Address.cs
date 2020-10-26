using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Views
{
    public class AddressViewModel
    {
        public DistrictViewModel District { get; set; } = new DistrictViewModel();

        public string Street { get; set; }

        public string Zip => District.Zip;

        public string Text => $"{District.FullName}{Street}";
    }
}
