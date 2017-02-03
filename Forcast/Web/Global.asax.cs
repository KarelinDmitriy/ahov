using System;
using System.IO;
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
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new AppAuthorizeAttribute());
		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			RouteConfig.RegisterRoutes(RouteTable.Routes);
		
			var json = Server.MapPath("~/App_Data/Matters.json");
			var file = File.ReadAllText(json);
			var kernel = new Container().CreateKernel(file);
			DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
		}

		protected void Application_AuthenticateRequest(object sender, EventArgs eventArgs)
		{
			var userInfo = this.GetCookie(CookiesConst.UserInfo);
			if (userInfo == null)
			{
				SetAnonymousUser();
				return;
			}
			var name = DecodeCookie(userInfo);
			var userCache = DependencyResolver.Current.GetService<IUserCache>();
			var user = userCache.FindUser(name);
			if (user == null)
			{
				var dbContext = DependencyResolver.Current.GetService<IDatabaseProvider>();
				var userEntity = dbContext.GetOne<UserEntity>(x => x.Login == name);
				if (userEntity == null)
				{
					SetAnonymousUser();
					return;
				}
				user = new AppUser(userEntity.UserId, userEntity.Login, userEntity.Fio, userEntity.Role);
			}
			SetUser(user);
		}

		private void SetUser(AppUser user)
		{
			Context.User = new AppPrincipal(user);
		}

		private void SetAnonymousUser()
		{
			var user = new AppPrincipal(new AppUser(0, "", "Аноним", "no one", false));
			Context.User = user;
		}

		private string DecodeCookie(string rawCookie)
		{
			return rawCookie;
		}
	}
}