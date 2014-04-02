using System;

using SQLite;

namespace Slim.Models
{
	public abstract class SlimModel
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
	}
}