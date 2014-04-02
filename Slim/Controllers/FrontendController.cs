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

namespace Slim.Controllers
{
	public class FrontendController : Controller
	{
		protected SlimUrlService urlService;

		protected SlimActivityService activityService;

		protected SlimGeoLogService geoService;

		public FrontendController(SlimUrlService urlService, SlimActivityService activityService, SlimGeoLogService geoService)
		{
			this.urlService = urlService;
			this.activityService = activityService;
			this.geoService = geoService;
		}

		private HomeViewModel CreateHomeViewModel() {
			HomeViewModel vm = new HomeViewModel();
			vm.CreateViewModel = new SlimUrlCreateViewModel();
			vm.ListViewModel = new SlimUrlListViewModel();
			vm.ListViewModel.SlimUrls = urlService.GetRecent();

			return vm;
		}

		public void Tiny()
		{

		}

		public ActionResult Index ()
		{
			ViewData ["Message"] = "Index";

			string ip = IpResolver.GetClientIpAddress(Request);

			Console.WriteLine(ip);
			Console.WriteLine(geoService.GetGeoIp("199.172.214.208"));
			Console.WriteLine(geoService.GetGeoIp(ip));

			return View (CreateHomeViewModel());
		}

		[HttpPost]
		public ActionResult IndexSubmit (SlimUrlCreateViewModel slimView)
		{
			ViewData["message"] = !ModelState.IsValid ? "Invalid form" : "Valid form";

			if (ModelState.IsValid) {
				SlimUrl s = urlService.GetByFullUrl(slimView.FullUrl);

				if (s == null) {
					// here i use the service to create and object then save it
					s = urlService.Create();
					s.FullUrl = slimView.FullUrl;
					urlService.Save(s);
					// here i use LogCreate to create a log and save it
					activityService.LogCreate(s);

					// Notes
					// I should probably pull all the creation methods
					// out into the controller. then i'll have a bunch of instantiation
					// and method calls. These calls should be refactored out into some
					// kind of over-arching SlimServer that handles communication
					// between all the sub services
				}

				return RedirectToAction("Index");
			}

			return View ("Index", CreateHomeViewModel());
		}

		public ActionResult Redirection (string hash)
		{

			SlimUrl s = urlService.GetByHash(hash);

			if (s == null) {
				Response.StatusCode = 404;
				return View("NotFound");
			}

			urlService.IncrementCount(s);
			activityService.LogRedirect(s);

			return View(s);
		}

	}
}

