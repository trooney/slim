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
//			var results = db.Query<SlimUrl>("select Hash from SlimUrl where Hash = ?", hash);
			return db.Table<SlimUrl>().Where( s => s.Hash.Equals(hash)).Count() > 0;
		}
						
		public SlimUrl GetByHash(string hash)
		{
			//db.Get<SlimUrl>(s => s.Hash.Equals(hash));
			return db.Table<SlimUrl>().Where( s => s.Hash.Equals(hash) ).FirstOrDefault();

		}

		public SlimUrl GetByFullUrl(string fullUrl)
		{
			// db.Get<SlimUrl>(s => s.FullUrl.Equals(fullUrl));
			return db.Table<SlimUrl>().Where( s => s.FullUrl.Equals(fullUrl) ).FirstOrDefault();
		}

		public IEnumerable<SlimUrl> GetRecent()
		{
			return db.Table<SlimUrl>().OrderByDescending(s => s.Id);
		}

		public void IncrementCount(string hash)
		{
			db.Execute("UPDATE SlimUrl SET Count = (Count + 1) WHERE Hash = ?", hash);
		}
	}
}

