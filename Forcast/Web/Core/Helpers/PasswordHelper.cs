using System;
using System.Security.Cryptography;
using System.Text;

namespace Web.Core.Helpers
{
	public static class PasswordHelper
	{
		public static string HashPassowrd(string password)
		{
			var sha = new SHA1CryptoServiceProvider();
			var passAdBytes = Encoding.UTF8.GetBytes(password);
			var hash = sha.ComputeHash(passAdBytes);
			var sb = new StringBuilder();
			foreach (var b in hash)
			{
				sb.Append($"{b:x2}");
			}
			return sb.ToString();
		}
	}
}