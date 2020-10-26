using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ApplicationCore.Helpers;
using Infrastructure.Views;

namespace ApplicationCore.Views
{
    public class CityViewModel : BaseRecordView
    {
        public string Code { get; set; }

        public string Title { get; set; }

        public ICollection<DistrictViewModel> Districts { get; set; } = new List<DistrictViewModel>();

    }

    public class DistrictViewModel : BaseRecordView
    {
        public string Zip { get; set; }

        public string Title { get; set; }

        public int CityId { get; set; }

        public string FullName { get; set; }

    }


}
