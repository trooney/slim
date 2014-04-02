using System;
using SQLite;

namespace Slim.Models
{
	public class SlimActivity : SlimModel
	{
		public enum ActivityTypes { Unknown, Created, Recreated, Redirected }

		public int SlimId { get; set; }

		public int ActivityType { get; set; }

		public DateTime CreatedDate { get; set; }
	}
}

