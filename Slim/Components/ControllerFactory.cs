using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Mvc;
using System.Web.SessionState;

using Slim.Controllers;
using Slim.Services;


namespace Slim.Components
{
	public class ControllerFactory : IControllerFactory
	{
		private DependencyManager dm;

		public ControllerFactory(DependencyManager serviceManager)
		{
			this.dm = serviceManager;
		}

		public IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
		{
			string className = string.Concat("Slim.Controllers.", controllerName, "Controller");

			Type type = Assembly.GetExecutingAssembly().GetType(className, false);

			// Default to ErrorController.HandleUnknownAction
			if (type == null) {
				type = Type.GetType("Slim.Controllers.ErrorController");

				return Activator.CreateInstance(type) as Controller;
			}

			return Activator.CreateInstance(type, new[] { dm }) as Controller;
		}

		public System.Web.SessionState.SessionStateBehavior GetControllerSessionBehavior(System.Web.Routing.RequestContext requestContext, string controllerName)
		{
			return SessionStateBehavior.Default;
		}

		public void ReleaseController(IController controller)
		{
			IDisposable disposable = controller as IDisposable;
			if (disposable != null) {
				disposable.Dispose();
			}
			controller = null;
		}

	}
}

