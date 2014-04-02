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
using TinyIoC;


namespace Slim.Components
{
	public class ControllerFactory : IControllerFactory
	{
		private TinyIoCContainer container;

		public ControllerFactory(TinyIoCContainer container)
		{
			this.container = container;
		}

		public IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
		{
			string className = string.Concat("Slim.Controllers.", controllerName, "Controller");

			Type type = Assembly.GetExecutingAssembly().GetType(className, false);

			return (IController)container.Resolve(type);
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
		}

	}
}

