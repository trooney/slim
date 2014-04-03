using System;
using System.Collections.Generic;
using System.Web;

using Slim.Components;
using Slim.Models;
using Slim.Repositories;

namespace Slim.Services
{
	public class ShortUrlService
	{
		protected ShortUrlRepository repository;

		protected TrackingService activityService;

		protected GeoIpService geoLogService;

		public ShortUrlService (ShortUrlRepository repository, TrackingService activityService, GeoIpService geoLogService)
		{
			this.repository = repository;
			this.activityService = activityService;
			this.geoLogService = geoLogService;
		}

		public ShortUrl Create()
		{
			return new ShortUrl();
		}

		public ShortUrl Save(ShortUrl s) {
		
			// New URLs need a unique hash
			if (s.Id == null) {
				s.Hash = GetUniqueHash();
			}	

			repository.Save(s);

			return s;
		}

		public string GetUniqueHash()
		{
			string hash;

			do {
				hash = ShortUrl.GenerateRandomHash();
			} while (repository.HashExists(hash));

			return hash;
		}

		public ShortUrl GetByHash(string hash)
		{
			return repository.GetByHash(hash);
		}

		public ShortUrl GetByFullUrl(string fullUrl)
		{
			return repository.GetByFullUrl(fullUrl);
		}

		public IEnumerable<ShortUrl> GetRecent()
		{
			return repository.GetRecent();
		}

		public void LogCreate(ShortUrl s, HttpRequestBase request)
		{
		}

		public void IncrementCount(ShortUrl s)
		{
			repository.IncrementCount(s);
		}

		public void LogRedirect(ShortUrl s, HttpRequestBase request)
		{
		}

	}
}

