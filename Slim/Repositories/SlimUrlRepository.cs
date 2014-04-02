using System;
using System.Collections.Generic;

using SQLite;
using Slim.Models;

namespace Slim.Repositories
{
	public class SlimUrlRepository : Repository<SlimUrl>
	{

		public SlimUrlRepository (SQLiteConnection db) : base(db)
		{
		}

		public bool HashExists(string hash)
		{
			return db.Table<SlimUrl>().Where( s => s.Hash.Equals(hash)).Count() > 0;
		}
						
		public SlimUrl GetByHash(string hash)
		{
			return db.Table<SlimUrl>().Where( s => s.Hash.Equals(hash) ).FirstOrDefault();

		}

		public SlimUrl GetByFullUrl(string fullUrl)
		{
			return db.Table<SlimUrl>().Where( s => s.FullUrl.Equals(fullUrl) ).FirstOrDefault();
		}

		public IEnumerable<SlimUrl> GetRecent()
		{
			return db.Table<SlimUrl>().OrderByDescending(s => s.Id);
		}

		public void IncrementCount(SlimUrl s)
		{
			// Increment object and db separately
			s.Count++;
			db.Execute("UPDATE SlimUrl SET Count = (Count + 1) WHERE Hash = ?", s.Hash);
		}
	}
}

