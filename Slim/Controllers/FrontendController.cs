using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

using Slim.Models;
using Slim.ViewModels;
using Slim.Utils;
using Slim.Services;

namespace Slim.Controllers
{
	public class FrontendController : Controller
	{
		protected SlimUrlService slimService;
		public string foo { get; set; }

		public FrontendController()
		{
			slimService = new SlimUrlService();
			Console.WriteLine(foo);
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
				slimService.GetByFullUrlOrCreate(slimView.FullUrl);

				return RedirectToAction("Index");
			}

			return View ("Index", CreateHomeViewModel());
		}

		public ActionResult Redirection (string hash)
		{
			slimService.IncrementCountForHash(hash);
			SlimUrl s = slimService.GetByHash(hash);

			return View(s);
		}
	}
}

