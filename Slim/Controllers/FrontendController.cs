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

		public FrontendController(DependencyManager dm)
		{
			this.urlService = dm.GetService<SlimUrlService>();
			this.activityService = dm.GetService<SlimActivityService>();
		}

		private HomeViewModel CreateHomeViewModel() {
			HomeViewModel vm = new HomeViewModel();
			vm.CreateViewModel = new SlimUrlCreateViewModel();
			vm.ListViewModel = new SlimUrlListViewModel();
			vm.ListViewModel.SlimUrls = urlService.GetRecent();

			return vm;
		}

		public ActionResult Index ()
		{
			ViewData ["Message"] = "Index";

			return View (CreateHomeViewModel());
		}

		[HttpPost]
		public ActionResult IndexSubmit (SlimUrlCreateViewModel slimView)
		{
			ViewData["message"] = !ModelState.IsValid ? "Invalid form" : "Valid form";

			if (ModelState.IsValid) {
				SlimUrl s = urlService.GetByFullUrl(slimView.FullUrl);

				if (s == null) {
					s = urlService.Create();
					s.FullUrl = slimView.FullUrl;
					urlService.Save(s);
					urlService.LogCreate(s);
				}

				return RedirectToAction("Index");
			}

			return View ("Index", CreateHomeViewModel());
		}

		public ActionResult Redirection (string hash)
		{

			SlimUrl s = urlService.GetByHash(hash);

			if (s == null) {
//				throw new HttpException(404, "Not found");
				Response.StatusCode = 404;
				return View("NotFound");

//				Response.StatusCode = 400;
//				Response.TrySkipIisCustomErrors = true;
//				return HttpNotFound();
			}

			urlService.LogRedirect(s);

			return View(s);
		}

	}
}

