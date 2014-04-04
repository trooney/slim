using System;
using System.Web;
using System.Web.Mvc;

using Slim.Components;

namespace Slim.Controllers
{
    public class ErrorController : Controller
    {
	
		private void RenderView(string name)
		{
			View(name).ExecuteResult(ControllerContext);
		}

		// @NOTE Unknown routes get mapped to this method
		protected override void HandleUnknownAction(string actionName) {
			Response.StatusCode = 404;
			RenderView("Error404");
		}

		public void Error404()
        {
			Response.StatusCode = 404;
			Response.TrySkipIisCustomErrors = true;
			RenderView("Error404");
        }
    }
}
