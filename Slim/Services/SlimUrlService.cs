using System;
using System.Collections.Generic;

using Slim.Components;
using Slim.Models;
using Slim.Repositories;

namespace Slim.Services
{
	public class SlimUrlService : Service
	{
		protected SlimUrlRepository repository;

		protected SlimActivityService activityService;

		public SlimUrlService (DependencyManager dm) : base(dm)
		{
			this.repository = dm.GetRepository<SlimUrlRepository>();
			this.activityService = dm.GetService<SlimActivityService>();
		}

		public SlimUrl Create()
		{
			return new SlimUrl();
		}

		public SlimUrl Save(SlimUrl s) {
		
			// New URLs need a unique hash
			if (s.Id == 0) {
				s.Hash = GetUniqueHash();
				s.CreatedDate = DateTime.Now;
			}	

			repository.Save(s);

			return s;
		}

		public string GetUniqueHash()
		{
			string hash;

			do {
				hash = SlimUrl.GenerateRandomHash();
			} while (repository.HashExists(hash));

			return hash;
		}

		public SlimUrl GetByHash(string hash)
		{
			return repository.GetByHash(hash);
		}

		public SlimUrl GetByFullUrl(string fullUrl)
		{
			return repository.GetByFullUrl(fullUrl);
		}

		public IEnumerable<SlimUrl> GetRecent()
		{
			return repository.GetRecent();
		}

		public void LogCreate(SlimUrl s)
		{
			activityService.LogCreate(s);
		}

		public void LogRedirect(SlimUrl s)
		{
			// Increment object and record in the database
			s.Count++;
			repository.IncrementCount(s.Hash);

			activityService.LogRedirect(s);
		}

	}
}

