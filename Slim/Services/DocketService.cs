using System;
using System.Collections.Generic;

using Slim.Components;
using Slim.Models;
using Slim.DTO;
using Slim.Repositories;

namespace Slim.Services
{
	public class DocketService
	{
		protected DocketRepository repository;

		public DocketService (DocketRepository repository)
		{
			this.repository = repository;
		}

		public void Save(Docket a)
		{
			repository.Save(a);
		}

		public Docket Create()
		{
			return new Docket();
		}

		public Docket CreateCreatedActivity()
		{
			var a = Create();
			a.Activity = DocketTypes.Created;
			return a;
		}

		public Docket CreateRedirectActivity()
		{
			var a = Create();
			a.Activity = DocketTypes.Redirected;
			return a;
		}

		public List<ShortUrlCountryUsage> GetCountryUsage(string hash)
		{
			return repository.GetCountryUsage(hash);
		}

	}
}