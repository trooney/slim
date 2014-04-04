using System;

using System.Web;
using System.Web.Mvc;

using Slim.Models;

namespace Slim.Helpers
{
	public static class SlimHtmlHelpers
	{
		public static string Test(string input)
		{
			return input;
		}

		public static MvcHtmlString SlimAbsoluteShortUrl(this HtmlHelper helper, string hash)
		{
			var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

			string url = String.Format("{0}://{1}{2}r/{3}",
				HttpContext.Current.Request.Url.Scheme,
				HttpContext.Current.Request.Url.Authority,
				urlHelper.Content("~"),
				hash
			);
			return new MvcHtmlString(url);
		}

		public static MvcHtmlString SlimAbsoluteShortUrl(this HtmlHelper helper, IShortUrl shortUrl)
		{
			return SlimHtmlHelpers.SlimAbsoluteShortUrl(helper, shortUrl.Hash);
		}

		public static MvcHtmlString SlimFriendlyShortUrl(this HtmlHelper helper, string hash)
		{
			var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

			string url = String.Format("{0}{1}r/{2}",
				HttpContext.Current.Request.Url.Authority,
				urlHelper.Content("~"),
				hash
			);
			return new MvcHtmlString(url);
		}

		public static MvcHtmlString SlimFriendlyShortUrl(this HtmlHelper helper, IShortUrl shortUrl)
		{
			return SlimHtmlHelpers.SlimFriendlyShortUrl(helper, shortUrl.Hash);
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

