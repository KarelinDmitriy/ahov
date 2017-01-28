using System;
using System.Runtime.Caching;

namespace Web.Core
{
	public interface IUserCache
	{
		AppUser FindUser(string login);
		void AddUser(AppUser user);
	}

	public class UserCache : IUserCache
	{
		private MemoryCache cache = new MemoryCache("users_cache");
		private const string keyPrefix = "knows_users";

		public AppUser FindUser(string login)
		{
			var key = GetKey(login);
			var user = cache.Get(key) as AppUser;
			return user;
		}

		public void AddUser(AppUser user)
		{
			var key = user.Name;
			cache.Add(key, user, DateTimeOffset.Now.AddHours(2));
		}

		private static string GetKey(string login) => $"{keyPrefix}_{login}";
	}
}