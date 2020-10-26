using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using System.Linq;
using ApplicationCore.Helpers;
namespace ApplicationCore.Models
{
    public class Post : BaseReviewable
    {
        public PostType Type { get; set; }

        public int CategoryId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Text { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public Category Category { get; set; }



        [NotMapped]
        public ICollection<UploadFile> Attachments { get; set; }


        public void LoadAttachments(IEnumerable<UploadFile> uploadFiles)
        {
            var attachments = uploadFiles.Where(x => x.PostType == this.Type && x.PostId == Id);
            this.Attachments = attachments.HasItems() ? attachments.ToList() : new List<UploadFile>();
        }
    }
}
