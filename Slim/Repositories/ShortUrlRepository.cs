using System;
using System.Collections.Generic;

using SQLite;
using Slim.Models;

namespace Slim.Repositories
{
	public class ShortUrlRepository : Repository<ShortUrl>
	{

		public ShortUrlRepository (SQLiteConnection db) : base(db)
		{
		}

		public bool HashExists(string hash)
		{
			return db.Table<ShortUrl>().Where( s => s.Hash.Equals(hash)).Count() > 0;
		}
						
		public ShortUrl GetByHash(string hash)
		{
			return db.Table<ShortUrl>().Where( s => s.Hash.Equals(hash) ).FirstOrDefault();

		}

		public ShortUrl GetByFullUrl(string fullUrl)
		{
			return db.Table<ShortUrl>().Where( s => s.FullUrl.Equals(fullUrl) ).FirstOrDefault();
		}

		public IEnumerable<ShortUrl> GetRecent()
		{
			return db.Table<ShortUrl>().OrderByDescending(s => s.Id);
		}

		public void IncrementCount(ShortUrl s)
		{
			// Increment object and db separately
			s.Count++;
			db.Execute("UPDATE ShortUrl SET Count = (Count + 1) WHERE Hash = ?", s.Hash);
		}
	}
}

