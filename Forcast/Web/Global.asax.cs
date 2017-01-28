using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AhovRepository;
using AhovRepository.Entity;
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

		protected void Application_AuthenticateRequest(object sender, EventArgs eventArgs)
		{
			var userInfo = this.GetCookie(CookiesConst.UserInfo);
			if (userInfo == null)
			{
				SetAnnonumusUser();
				return;
			}
			var name = DecodeCookie(userInfo);
			var userCache = DependencyResolver.Current.GetService<IUserCache>();
			var user = userCache.FindUser(name);
			if (user == null)
			{
				var dbContext = DependencyResolver.Current.GetService<IDatabaseProvider>();
				var userEntity = dbContext.GetOne<UserEntity>(x => x.Name == name);
				if (userEntity == null)
				{
					SetAnnonumusUser();
					return;
				}
				user = new AppUser(userEntity.Name, userEntity.Name);
			}
			SetUser(user);
		}

		private void SetUser(AppUser user)
		{
			Context.User = new AppPrincipal(user);
		}

		private void SetAnnonumusUser()
		{
			var user = new AppPrincipal(new AppUser("", "Аноним", false));
			Context.User = user;
		}

		private string DecodeCookie(string rawCookie)
		{
			return rawCookie;
		}
	}
}