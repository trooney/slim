using System;
using SQLite;

namespace Slim.Models
{
	public class SlimActivity : Model
	{
		public readonly string Created = "created";

		public readonly string Redirected = "redirected";

		public int SlimId { get; set; }

		public string Action { get; set; }

		public DateTime CreatedDate { get; set; }
	}
}

