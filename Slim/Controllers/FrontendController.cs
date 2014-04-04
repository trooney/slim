using System;
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

		private HomeViewModel CreateHomeViewModel() {
			HomeViewModel vm = new HomeViewModel();

			vm.ListViewModel.ShortUrls = shortUrlService.GetRecent();

			return vm;
		}

		public ActionResult Home ()
		{

			return View (CreateHomeViewModel());
		}

		public ActionResult Shorten (ShortUrlCreateViewModel slimView)
		{
			if (Request.HttpMethod == "POST" && ModelState.IsValid) {
				ShortUrl s = shortUrlService.GetByFullUrl(slimView.FullUrl);

				if (s == null) {
					s = shortUrlService.Create();
					s.FullUrl = slimView.FullUrl;
					shortUrlService.Save(s);

					var t = trackingService.CreateCreatedActivity();
					t.SlimId = s.Id;
					t.Ip = IpResolver.GetClientIpAddress(Request);
					trackingService.Save(t);

					geoService.GetGeoIpAsync(t, ((Tracking updated) => {
						trackingService.Save(updated);
					}));
				}

				return RedirectToAction("Shorten");
			}

			return View (CreateHomeViewModel());
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