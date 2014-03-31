using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

using Slim.Models;
using Slim.ViewModels;
using Slim.Helpers;
using Slim.Services;

namespace Slim.Controllers
{
	public class FrontendController : Controller
	{
		protected SlimUrlService slimService;

		public FrontendController()
		{
			slimService = new SlimUrlService();
		}

		private HomeViewModel CreateHomeViewModel() {
			HomeViewModel vm = new HomeViewModel();
			vm.CreateViewModel = new SlimUrlCreateViewModel();
			vm.ListViewModel = new SlimUrlListViewModel();
			vm.ListViewModel.SlimUrls = slimService.GetRecent();

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
				SlimUrl slimUrl;
				try {
					slimUrl = slimService.GetByFullUrl(slimView.FullUrl);
				} catch (System.InvalidOperationException) {
					slimUrl = new SlimUrl{ FullUrl = slimView.FullUrl};
					slimService.Save(slimUrl);
				}

				return RedirectToAction("Index");
			}

			return View ("Index", CreateHomeViewModel());
		}

		public ActionResult Redirect (string hash)
		{
			SlimUrl slimUrl;
			try {
				slimUrl = slimService.GetByHash(hash);
			} catch (System.InvalidOperationException) {
				Console.WriteLine("Cannot find SlimUrl {0}", hash);
				slimUrl = new SlimUrl();
			}
			return View(slimUrl);
		}
	}
}

