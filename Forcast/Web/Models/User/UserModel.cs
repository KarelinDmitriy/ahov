using System.ComponentModel;
using AhovRepository;

namespace Web.Models.User
{
	public class UserModel
	{
		public UserModel()
		{
			Info = new WebUser();
		}

		public UserModel(WebUser user)
		{
			Info = user;
		}
		public WebUser Info { get; set; }

		[DisplayName("Пароль")]
		public string Password { get; set; }
	}
}