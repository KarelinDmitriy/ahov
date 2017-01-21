using System.ComponentModel;
using AhovRepository.Entity;

namespace Web.Models.User
{
	public class UserModel
	{
		public UserModel()
		{
			Info = new UserEntity();
		}

		public UserModel(UserEntity user)
		{
			Info = user;
		}
		public UserEntity Info { get; set; }

		[DisplayName("Пароль")]
		public string Password { get; set; }
	}
}