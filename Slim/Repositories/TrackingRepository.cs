using System;

using SQLite;
using Slim.Models;

namespace Slim.Repositories
{
	public class TrackingRepository : Repository<Tracking>
	{
		public TrackingRepository (SQLiteConnection db)
		{
			this.db = db;
		}

	}
}

