using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

using AutoMapper;
using Slim.Models;
using Slim.ViewModels;
using Slim.Services;
using Slim.Components;

// tmp
using Slim.Repositories;

namespace Slim.Controllers
{
	public class FrontendController : Controller
	{
		protected ShortUrlService shortUrlService;

		protected DocketService docketService;

		protected GeoIpService geoService;

		public DocketRepository t;

		public FrontendController(ShortUrlService urlService, DocketService docketService, GeoIpService geoService, DocketRepository t)
		{
			this.shortUrlService = urlService;
			this.docketService = docketService;
			this.geoService = geoService;

			this.t = t;
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

					var t = docketService.CreateCreatedActivity();
					t.ShortUrlId = s.Id;
					t.Ip = IpResolver.GetClientIpAddress(Request);
					docketService.Save(t);

					geoService.GetGeoIpAsync(t, ((Docket updated) => {
						docketService.Save(updated);
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

			var t = docketService.CreateRedirectActivity();
			t.ShortUrlId = s.Id;
			t.Ip = IpResolver.GetClientIpAddress(Request);
			docketService.Save(t);

			geoService.GetGeoIpAsync(t, ((Docket updated) => {
				docketService.Save(updated);
			}));

			#if DEBUG
			return View(s);
			#else
			return RedirectPermanent(s.FullUrl);
			#endif
		}

	}
}