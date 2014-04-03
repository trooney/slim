using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

using Slim.Models;

namespace Slim.Controllers
{
	public class ApiController : Controller
	{
		public ActionResult Index ()
		{
			var db = new SQLite.SQLiteConnection("App_Data/slim.sqlite");
			IEnumerable<ShortUrl> records = db.Query<ShortUrl> ("select * from SlimUrl");

			return Json(records, JsonRequestBehavior.AllowGet);
		}
	}
}

