using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject.Web.Mvc;
using Web.Core;

namespace Web
{
	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			var kernel = new Container().CreateKernel();
			DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
		}
	}
}