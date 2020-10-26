using ApplicationCore.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    
    public class PostEditForm
    {
        public PostViewModel Post { get; set; }

        public ICollection<CompanyViewModel> Companies { get; set; } = new List<CompanyViewModel>();
    }
}
