using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Models
{
	public enum PostType
	{
		Default = 0,
		Notice = 1,
		None = -1
	}

	public class UploadFile : BaseUploadFile
	{
		public PostType PostType { get; set; } = PostType.None;
		public int PostId { get; set; }

	}
}
