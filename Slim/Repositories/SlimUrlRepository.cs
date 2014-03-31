using System;
using System.Collections.Generic;
using Slim.Models;

namespace Slim.Repositories
{
	public class SlimUrlRepository
	{
		protected SQLite.SQLiteConnection db;

		public SlimUrlRepository ()
		{
			db = new SQLite.SQLiteConnection("App_Data/slim.sqlite");
		}
			
		public void Insert(SlimUrl s)
		{
			 db.Insert(s);
		}

		public void Update(SlimUrl s)
		{
			db.Update(s);
		}

		public bool HashExists(string hash)
		{
			var results = db.Query<SlimUrl>("select Hash from SlimUrl where Hash = ?", hash);
			return results.Count >= 1;
		}
						
		public SlimUrl GetByHash(string hash)
		{
			return db.Get<SlimUrl>(s => s.Hash.Equals(hash));
		}

		public SlimUrl GetByFullUrl(string fullUrl)
		{
			return db.Get<SlimUrl>(s => s.FullUrl.Equals(fullUrl));
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

