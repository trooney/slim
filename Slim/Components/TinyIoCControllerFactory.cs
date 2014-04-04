using System;
using System.Reflection;
using System.Web.Routing;
using System.Web.Mvc;
using System.Web.SessionState;

using TinyIoC;

namespace Slim.Components
{
	public class TinyIoCControllerFactory : IControllerFactory
	{
		private TinyIoCContainer container;

		public TinyIoCControllerFactory(TinyIoCContainer container)
		{
			this.container = container;
		}

		public IController CreateController(RequestContext requestContext, string controllerName)
		{
			string className = string.Concat("Slim.Controllers.", controllerName, "Controller");

			Type type = Assembly.GetExecutingAssembly().GetType(className, false);

			return (IController)container.Resolve(type);
		}

		public System.Web.SessionState.SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
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