using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

using Slim.Services;
using Slim.Repositories;

namespace Slim.Controllers
{
	public class ApiController : Controller
	{
		protected ShortUrlService shortUrlService;

		protected DocketService docketService;

		protected GeoIpService geoService;

		public ApiController(ShortUrlService urlService, DocketService docketervice, GeoIpService geoService)
		{
			this.shortUrlService = urlService;
			this.docketService = docketervice;
			this.geoService = geoService;
		}

		public JsonResult Index ()
		{
			return Json("null", JsonRequestBehavior.AllowGet);
		}

		public JsonResult ShortUrlCountryUsage(string hash)
		{
			var results = docketService.GetCountryUsage(hash);

			if (results.Count == 0) {
				HttpContext.Response.StatusCode = 404;

				return Json(new { message = "not usage found for this short url" }, JsonRequestBehavior.AllowGet);
			}

			return Json(results, JsonRequestBehavior.AllowGet);
		}
	}
}

