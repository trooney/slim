using System;

using Slim.Components;
using Slim.Models;
using Slim.Repositories;


namespace Slim.Services
{
	public class SlimActivityService
	{
		protected SlimActivityRepository repository;

		public SlimActivityService (SlimActivityRepository repository)
		{
			this.repository = repository;
		}

		public SlimActivity Create()
		{
			return new SlimActivity();
		}

		public void Save(SlimActivity a)
		{
			repository.Save(a);
		}

		public void LogCreate(SlimUrl s)
		{
			var a = Create();
			a.Activity = ActivityTypes.Created;
			a.SlimId = s.Id;

			Save(a);
		}

		public void LogRedirect(SlimUrl s)
		{
			var a = Create();
			a.Activity = ActivityTypes.Redirected;
			a.SlimId = s.Id;

			Save(a);
		}
	}
}

