using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

using AutoMapper;
using Slim.Models;
using Slim.ViewModels;
using Slim.Services;
using Slim.Components;

namespace Slim.Controllers
{
	public class FrontendController : Controller
	{
		protected ShortUrlService shortUrlService;

		protected TrackingService trackingService;

		protected GeoIpService geoService;

		public FrontendController(ShortUrlService urlService, TrackingService activityService, GeoIpService geoService)
		{
			this.shortUrlService = urlService;
			this.trackingService = activityService;
			this.geoService = geoService;
		}

		private Home CreateHomeViewModel() {
			Home vm = new Home();

			vm.RecentShortUrls = shortUrlService.GetRecent();

			return vm;
		}

		public ActionResult Home ()
		{
			Home vm = new Home();

			vm.RecentShortUrls = shortUrlService.GetRecent();

			return View (vm);
		}

		public ActionResult Shorten (ShortUrlCreate shortUrlCreate)
		{
			Shorten vm = new Slim.ViewModels.Shorten();

			if (Request.HttpMethod == "POST" && ModelState.IsValid) {

				// Prevent form value repopulation
				ModelState.Clear();

				ShortUrl s = shortUrlService.GetByFullUrl(shortUrlCreate.FullUrl);

				if (s == null) {
					s = shortUrlService.Create();
					s.FullUrl = shortUrlCreate.FullUrl;
					shortUrlService.Save(s);

					var t = trackingService.CreateCreatedActivity();
					t.SlimId = s.Id;
					t.Ip = IpResolver.GetClientIpAddress(Request);
					trackingService.Save(t);

					geoService.GetGeoIpAsync(t, ((Tracking updated) => {
						trackingService.Save(updated);
					}));

				}

				vm.LastShortUrls.Add(s);

				vm.RecentShortUrls = shortUrlService.GetRecentExcluding(s);

			} else {
				vm.RecentShortUrls = shortUrlService.GetRecent();
			}

			return View (vm);
		}

		public ActionResult Redirection (string hash)
		{
			ShortUrl s = shortUrlService.GetByHash(hash);

			if (s == null) {
				Response.StatusCode = 404;
				return View("NotFound");
			}

			shortUrlService.IncrementCount(s);

			var t = trackingService.CreateRedirectActivity();
			t.SlimId = s.Id;
			t.Ip = IpResolver.GetClientIpAddress(Request);
			trackingService.Save(t);

			geoService.GetGeoIpAsync(t, ((Tracking updated) => {
				trackingService.Save(updated);
			}));

			return View(s);
		}

	}
}