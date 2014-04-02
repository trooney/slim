using System;

using SQLite;
using Slim.Models;

namespace Slim.Repositories
{
	public class SlimGeoLogRepository : Repository<SlimGeoLog>
	{
		public SlimGeoLogRepository  (SQLiteConnection db) : base(db)
		{
		}
	}
}