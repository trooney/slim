using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Slim.Components;

namespace Slim.Controllers
{
    public class ErrorController : Controller
    {
		// @NOTE ErrorController will never accept any parameters
		public ErrorController()
		{
		}

		// @NOTE Unknown routes get mapped to this method
		protected override void HandleUnknownAction(string actionName) {
			Response.StatusCode = 404;
			string path = Path.Combine("~", "Views", "Error", "Error404.cshtml");
			View(path).ExecuteResult(ControllerContext);
		}

		public ActionResult Error404()
        {
			Response.StatusCode = 404;
			Response.TrySkipIisCustomErrors = true;
            return View ();
        }
    }
}
