using System;

using Slim.Components;
using Slim.Models;
using Slim.Repositories;


namespace Slim.Services
{
	public class SlimActivityService : Service
	{
		protected SlimActivityRepository repository;

		public SlimActivityService (DependencyManager dm) : base(dm)
		{
			this.repository = dm.GetRepository<SlimActivityRepository>();
		}

		public SlimActivity Create()
		{
			return new SlimActivity();
		}

		public void Save(SlimActivity a)
		{
			if (a.Id == 0) {
				a.CreatedDate = DateTime.Now;
			}

			repository.Save(a);
		}

		public void LogCreate(SlimUrl s)
		{
			var a = Create();
			a.ActivityType = (int)SlimActivity.ActivityTypes.Created;
			a.SlimId = s.Id;

			Save(a);
		}

		public void LogRedirect(SlimUrl s)
		{
			var a = Create();
			a.ActivityType = (int)SlimActivity.ActivityTypes.Redirected;
			a.SlimId = s.Id;

			Save(a);
		}
	}
}

