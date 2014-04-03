using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

using Slim.Models;
using Slim.ViewModels;
using Slim.Components;
using Slim.Services;
using Slim.Repositories;

using System.Threading.Tasks;

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

		private string GetRequestIp()
		{
			return IpResolver.GetClientIpAddress(Request);
		}

		private HomeViewModel CreateHomeViewModel() {
			HomeViewModel vm = new HomeViewModel();
			vm.CreateViewModel = new ShortUrlCreateViewModel();
			vm.ListViewModel = new ShortUrlListViewModel();
			vm.ListViewModel.SlimUrls = shortUrlService.GetRecent();

			return vm;
		}

		public ActionResult Index ()
		{
			ViewData ["Message"] = "Index";

			return View (CreateHomeViewModel());
		}

//		public async Task<ActionResult> Index ()
//		{
//			ViewData ["Message"] = "Index";
//
//			string responseFromServer = await FrontendController.GetSomething();
//
//			return View (CreateHomeViewModel());
//		}
//
//		public static async Task<string> GetSomething()
//		{
//			System.Threading.Thread.Sleep(5000);
//			Console.WriteLine("Async thing finished");
//			return "foo";
//		}

		[HttpPost]
		public ActionResult IndexSubmit (ShortUrlCreateViewModel slimView)
		{
			ViewData["message"] = !ModelState.IsValid ? "Invalid form" : "Valid form";

			if (ModelState.IsValid) {
				ShortUrl s = shortUrlService.GetByFullUrl(slimView.FullUrl);

				if (s == null) {
					s = shortUrlService.Create();
					s.FullUrl = slimView.FullUrl;
					shortUrlService.Save(s);

					var t = trackingService.CreateCreatedActivity();
					t.SlimId = s.Id;
					t.Ip = GetRequestIp();
					trackingService.Save(t);

					// This will perform binding async-like
					//geoService.BindGeoIpData(t, t.Ip);

					geoService.BindGeoIpDataUsingAsyncThenSave(trackingService, t, t.Ip);


				}

				return RedirectToAction("Index");
			}

			return View ("Index", CreateHomeViewModel());
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
			t.Ip = GetRequestIp();
//			geoService.BindGeoIpData(t, t.Ip);
			trackingService.Save(t);

			geoService.BindGeoIpDataUsingAsyncThenSave(trackingService, t, t.Ip);

			return View(s);
		}

	}
}

