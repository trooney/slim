using System;

using SQLite;

namespace Slim.Models
{
	public abstract class Model
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
	}
}