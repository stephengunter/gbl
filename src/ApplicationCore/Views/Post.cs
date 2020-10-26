using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ApplicationCore.Helpers;
using Infrastructure.Views;

namespace ApplicationCore.Views
{
    public class PostViewModel : BaseReviewableView
    {
        public string UserId { get; set; }

        public int CategoryId { get; set; }

        [StringLength(30, ErrorMessage = "標題長度超出限制")]
        public string Title { get; set; }

        public string Content { get; set; }

        public string Text { get; set; }

        public ICollection<AttachmentViewModel> Attachments { get; set; } = new List<AttachmentViewModel>();
    }

}
