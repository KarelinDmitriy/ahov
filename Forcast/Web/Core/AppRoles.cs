using System.Collections.Generic;

namespace Web.Core
{
	public static class AppRoles
	{
		public const string Admin = nameof(Admin);
		public const string Client = nameof(Client);

		public static List<string> AllowRoles = new List<string>() {Admin, Client};
	}
}