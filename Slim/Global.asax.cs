using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

using SQLite;
using Slim.Components;
using TinyIoC;

namespace Slim
{
	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterRoutes (RouteCollection routes)
		{
			routes.IgnoreRoute ("{resource}.axd/{*pathInfo}");

			routes.MapRoute (
				"Error404",
				"Error404/{action}/{id}",
				new { controller = "Error", action = "Error404", id = UrlParameter.Optional }
			);

			routes.MapRoute (
				"SlimUrlRedirect",
				"r/{hash}",
				new { controller = "Frontend", action = "Redirection", hash = "" }
			);

			routes.MapRoute (
				"Default",
				"{controller}/{action}/{id}",
				new { controller = "Frontend", action = "Index", id = "" }
			);
				
		}

		public static void RegisterGlobalFilters (GlobalFilterCollection filters)
		{
			filters.Add (new HandleErrorAttribute ());
		}

		protected void Application_Start ()
		{
			AreaRegistration.RegisterAllAreas ();
			RegisterGlobalFilters (GlobalFilters.Filters);
			RegisterRoutes (RouteTable.Routes);

			// Create container
			TinyIoCContainer container = TinyIoCContainer.Current;

			// Set database on container
			SQLiteConnection db = GetDatabase();
			container.Register<SQLiteConnection>(db);

			// Create controller factory and in container
			IControllerFactory factory = new ControllerFactory(container);
			ControllerBuilder.Current.SetControllerFactory(factory);
		}

		private SQLiteConnection GetDatabase()
		{
			string connectionString = WebConfigurationManager.ConnectionStrings["slimDatabase"].ConnectionString;
			string path = Server.MapPath(Path.Combine ("App_Data", connectionString));

			var db = new SQLite.SQLiteConnection(path);

			return db;
		}

	}
}
