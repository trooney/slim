using System;
using System.Collections.Generic;

using Slim.Repositories;
using Slim.Models;
using Slim.Helpers;

namespace Slim.Services
{
	public class SlimUrlService
	{
		protected SlimUrlRepository repository;

		public string GenerateRandomHash()
		{
			string hash;

			do {
				int hashId = new Random().Next(100000000,999999999);
				hash = ShortUrl.Shrink(hashId);
			} while (repository.HashExists(hash));

			return hash;
		}

		public SlimUrlService ()
		{
			repository = new SlimUrlRepository();
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

		public SlimUrl Save(SlimUrl s) {
			s.CreatedDate = DateTime.Now;

			Console.WriteLine(s);

			repository.Insert(s);

			s.Hash = GenerateRandomHash();

			Console.WriteLine(s);

			repository.Update(s);

			return s;
		}

	}
}

