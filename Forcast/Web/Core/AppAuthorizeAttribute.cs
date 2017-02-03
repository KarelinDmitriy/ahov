using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web.Core
{
	public class AppAuthorizeAttribute : AuthorizeAttribute
	{
		private readonly string[] roles;

		public AppAuthorizeAttribute(params string[] roles)
		{
			this.roles = roles;
		}

		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			var user = (httpContext.User.Identity as AppUser);
			if (user == null)
				return false;
			var localRoles = roles.Length > 0 ? roles : AppRoles.AllowRoles.ToArray();
			return user.IsAuthenticated && localRoles.Any(x => user.Role == x);
		}

		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			var values = new RouteValueDictionary(new
			{
				action = "Login",
				controller = "User"
			});

			filterContext.Result = new RedirectToRouteResult(values);
		}
	}
}