using System;

using SQLite;
using Slim.Models;

namespace Slim.Repositories
{
	public class SlimActivityRepository : Repository<SlimActivity>
	{
		public SlimActivityRepository (SQLiteConnection db) : base(db)
		{
		}

	}
}

