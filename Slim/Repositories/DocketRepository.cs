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

		public List<ShortUrlCountryUsage> GetCountryUsage(string hash)
		{
			var results = db.Query<ShortUrlCountryUsage>(@"
				SELECT s.Hash, Count(d.CountryCode) as Count, d.CountryCode, d.CountryName 
				FROM ShortUrl s
				LEFT JOIN Docket d
				ON s.Id = d.ShortUrlId
				WHERE s.Hash = ?
				GROUP BY d.CountryCode, d.CountryName, s.Hash
				HAVING Count(d.CountryCode) > 0
				ORDER BY Count(d.CountryCode) DESC
			", hash);

			return results;
		}
	}
}

