using System;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

using SQLite;
using TinyIoC;
using AutoMapper;
using Slim.Components;

namespace Slim
{
	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterRoutes (RouteCollection routes)
		{
			routes.IgnoreRoute("favicon.ico");

			routes.IgnoreRoute ("{resource}.axd/{*pathInfo}");

			routes.MapRoute (
				"Error404",
				"Error404/{action}/{id}",
				new { controller = "Error", action = "Error404", id = UrlParameter.Optional }
			);

			routes.MapRoute (
				"Home",
				"",
				new { controller = "Frontend", action = "Home" }
			);

			routes.MapRoute (
				"Redirection",
				"r/{hash}",
				new { controller = "Frontend", action = "Redirection", hash = "" }
			);

			routes.MapRoute (
				"Shorten",
				"shorten",
				new { controller = "Frontend", action = "Shorten" }
			);

			routes.MapRoute (
				"Api",
				"api/{hash}/countries",
				new { controller = "Api", action = "ShortUrlCountryUsage", hash = "" }
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


			// Create controller factory and in container
			var container = GetContainer();
			IControllerFactory factory = new TinyIoCControllerFactory(container);
			ControllerBuilder.Current.SetControllerFactory(factory);

			// Configure AutoMapper
			SetupAutomapper();
		}

		private SQLiteConnection GetDatabase()
		{
			string connectionString = WebConfigurationManager.ConnectionStrings["slimDatabase"].ConnectionString;
			string path = Server.MapPath(Path.Combine ("App_Data", connectionString));

			var db = new SQLite.SQLiteConnection(path);

			db.CreateTable<Slim.Models.ShortUrl>();
			db.CreateTable<Slim.Models.Docket>();

			return db;
		}
			

		private TinyIoCContainer GetContainer()
		{
			TinyIoCContainer container = TinyIoCContainer.Current;

			// Set database on container
			SQLiteConnection db = GetDatabase();
			container.Register<SQLiteConnection>(db);

			return container;
		}

		private void SetupAutomapper()
		{
			Mapper.CreateMap<Slim.DTO.GeoIp, Slim.Models.Docket>();
		}

	}
}
