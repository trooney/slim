using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Slim.Controllers
{
	public class ApiController : Controller
	{
		public ActionResult Index ()
		{
			return Json("null", JsonRequestBehavior.AllowGet);
		}
	}
}

