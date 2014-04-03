using System;

using Slim.Components;
using Slim.Models;
using Slim.Repositories;

namespace Slim.Services
{
	public class TrackingService
	{
		protected TrackingRepository repository;

		public TrackingService (TrackingRepository repository)
		{
			this.repository = repository;
		}

		public void Save(Tracking a)
		{
			repository.Save(a);
		}

		public Tracking Create()
		{
			return new Tracking();
		}

		public Tracking CreateCreatedActivity()
		{
			var a = Create();
			a.Activity = ActivityTypes.Created;
			return a;
		}

		public Tracking CreateRedirectActivity()
		{
			var a = Create();
			a.Activity = ActivityTypes.Redirected;
			return a;
		}


	}
}

