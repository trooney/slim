using System;
using System.Collections.Generic;

using Slim.Repositories;
using Slim.Models;
using Slim.Utils;

namespace Slim.Services
{
	public class SlimUrlService
	{
		protected SlimUrlRepository repository;

		public SlimUrlService ()
		{
			repository = new SlimUrlRepository();
		}

		public SlimUrl Save(SlimUrl s) {
			s.CreatedDate = DateTime.Now;

			repository.Insert(s);

			s.Hash = GetUniqueHash();

			repository.Update(s);

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

		public SlimUrl GetByFullUrlOrCreate(string fullUrl) {
			SlimUrl s;

			try {
				s = GetByFullUrl(fullUrl);
			} catch (System.InvalidOperationException) {
				s = new SlimUrl{ FullUrl = fullUrl};
				Save(s);
			}
				
			return s;
		}

		public void IncrementCountForHash(string hash)
		{
			repository.IncrementCount(hash);
		}

	}
}

