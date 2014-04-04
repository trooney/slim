using System;

using System.Web;
using System.Web.Routing;
using System.Web.Mvc;

using Slim.Models;

namespace Slim.Helpers
{
	public static class SlimUrlHelper
	{
		public static string RedirectAction = "r";

		// return http://hostname/r/{hash}
		public static MvcHtmlString SlimAbsoluteShortUrl(this HtmlHelper helper, string hash)
		{
			var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

			string url = String.Format("{0}://{1}{2}{3}/{4}",
				HttpContext.Current.Request.Url.Scheme,
				HttpContext.Current.Request.Url.Authority,
				urlHelper.Content("~"),
				SlimUrlHelper.RedirectAction,
				hash
			);
			return new MvcHtmlString(url);
		}

		public static MvcHtmlString SlimAbsoluteShortUrl(this HtmlHelper helper, IShortUrl shortUrl)
		{
			return SlimUrlHelper.SlimAbsoluteShortUrl(helper, shortUrl.Hash);
		}

		// return hostname/r/{hash}
		public static MvcHtmlString SlimFriendlyShortUrl(this HtmlHelper helper, string hash)
		{
			var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

			string url = String.Format("{0}{1}{2}/{3}",
				HttpContext.Current.Request.Url.Authority,
				urlHelper.Content("~"),
				SlimUrlHelper.RedirectAction,
				hash
			);
			return new MvcHtmlString(url);
		}

		public static MvcHtmlString SlimFriendlyShortUrl(this HtmlHelper helper, IShortUrl shortUrl)
		{
			return SlimUrlHelper.SlimFriendlyShortUrl(helper, shortUrl.Hash);
		}

		public static MvcHtmlString SlimHostnameForUrl(this HtmlHelper helper, string url)
		{
			Uri uri;

			if (Uri.TryCreate(url, UriKind.Absolute, out uri) == false) {
				return new MvcHtmlString("");
			}

			return new MvcHtmlString(uri.Host);
		}

	}
}

