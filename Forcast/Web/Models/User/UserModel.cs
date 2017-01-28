using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}