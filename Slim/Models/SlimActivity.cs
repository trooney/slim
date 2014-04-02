using System;
using SQLite;

namespace Slim.Models
{
	public enum ActivityTypes { 
		Unknown = 0, 
		Created = 1, 
		Recreated = 2,
		Redirected = 3
	}

	public class SlimActivity : IModel
	{
		[PrimaryKey, AutoIncrement]
		public int? Id { get; set; }
		public ActivityTypes Activity { get; set; }
		public DateTime? CreatedDate { get; set; }
		public int? SlimId { get; set; }

		public SlimActivity()
		{
			CreatedDate = DateTime.Now;
		}

	}
}

