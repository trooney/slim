using System;
using System.Collections.Generic;

using SQLite;
using Slim.Models;
using Slim.DTO;

namespace Slim.Repositories
{

	public class DocketRepository : Repository<Docket>
	{
		public DocketRepository (SQLiteConnection db)
		{
			this.db = db;
		}

		public List<ShortUrlUsage> GetUsage(string hash)
		{
			var results = db.Query<ShortUrlUsage>(@"
				SELECT s.Hash, Count(d.City) as Count, d.City, d.CountryCode, d.CountryName 
				FROM ShortUrl s
				LEFT JOIN Docket d
				ON s.Id = d.ShortUrlId
				WHERE s.Hash = ?
				GROUP BY d.City, d.CountryCode, d.CountryName, s.Hash
				HAVING Count(d.CountryCode) > 0
				ORDER BY Count(d.CountryCode) DESC
			", hash);

			return results;
		}
	}
}

