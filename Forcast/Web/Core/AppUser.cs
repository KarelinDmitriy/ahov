using System.Security.Principal;

namespace Web.Core
{
	public class AppPrincipal : IPrincipal
	{
		public AppPrincipal(AppUser user)
		{
			Identity = user;
		}

		public bool IsInRole(string role)
		{
			return true;
		}

		public IIdentity Identity { get; }
	}

	public class AppUser : IIdentity
	{
		public AppUser(int userId, string login, string name, bool isAuthenticated = true)
		{
			Name = login;
			UserName = name;
			IsAuthenticated = isAuthenticated;
			UserId = userId;
		}

		public string Name { get; }
		public string AuthenticationType { get; set; }
		public bool IsAuthenticated { get; }
		public string UserName { get; }
		public int UserId { get; set; }
	}
}