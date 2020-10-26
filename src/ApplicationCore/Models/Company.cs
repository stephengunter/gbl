using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Infrastructure.Entities;

namespace ApplicationCore.Models
{
    public class Company : BaseCategory
    {
        public string UserId { get; set; }

        public bool Top { get; set; }

        public int DistrictId { get; set; }

        public string Street { get; set; }

        public string Phone { get; set; }

        public District District { get; set; }

        [NotMapped]
        public ICollection<Company> SubItems { get; private set; }

        public void LoadSubItems(IEnumerable<Company> subItems)
        {
            SubItems = subItems.Where(item => item.ParentId == this.Id).OrderBy(item => item.Order).ToList();

            foreach (var item in SubItems)
            {
                item.LoadSubItems(subItems);
            }
        }

    }
}
