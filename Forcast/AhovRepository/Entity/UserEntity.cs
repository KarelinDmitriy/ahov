using System.ComponentModel;
using FluentNHibernate.Mapping;

namespace AhovRepository.Entity
{
	public class UserEntity
	{
		public virtual int UserId { get; set; }
		[DisplayName("Логин")]
		public virtual string Login { get; set; }
		[DisplayName("Пароль")]
		public virtual string PasswordHash { get; set; }
		[DisplayName("Email")]
		public virtual string Email { get; set; }
		[DisplayName("Роль")]
		public virtual string Role { get; set; }
		[DisplayName("Фамилия, имя, отчество")]
		public virtual string Fio { get; set; }
	}

	public class UserMap : ClassMap<UserEntity>
	{
		public UserMap()
		{
			Id(x => x.UserId).Column("UserId");
			Map(x => x.Login).Column("UserName");
			Map(x => x.Email).Column("Email");
			Map(x => x.PasswordHash).Column("PasswordHash");
			Map(x => x.Role).Column("Role");
			Map(x => x.Fio).Column("Fio");
			Table("User");
		}
	}
}