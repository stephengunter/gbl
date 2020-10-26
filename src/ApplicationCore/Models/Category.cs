using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Infrastructure.Entities;

namespace ApplicationCore.Models
{
    public class Category : BaseCategory
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public string View { get; set; }

        public string Icon { get; set; }

        public ICollection<Post> Posts { get; set; }

        [NotMapped]
        public ICollection<Category> SubItems { get; private set; }


        public void LoadSubItems(IEnumerable<Category> subItems)
        {
            SubItems = subItems.Where(item => item.ParentId == this.Id).OrderBy(item => item.Order).ToList();

            foreach (var item in SubItems)
            {
                item.LoadSubItems(subItems);
            }
        }
    }
}
