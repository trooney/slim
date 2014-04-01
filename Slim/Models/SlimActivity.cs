using System;
using SQLite;

namespace Slim.Models
{
	public class SlimActivity : Model
	{
		public static readonly string WasCreated = "created";

		public static readonly string WasRedirected = "redirected";

		public int SlimId { get; set; }

		public string Action { get; set; }

		public DateTime CreatedDate { get; set; }
	}
}

