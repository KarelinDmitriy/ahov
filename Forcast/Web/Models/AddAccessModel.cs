using System.ComponentModel;

namespace Web.Models
{
	public class AddAccessModel
	{
		public string ObjectId { get; set; }
		[DisplayName("Логин")]
		public string Login { get; set; }
		[DisplayName("Тип доступа")]
		public string AccessType { get; set; }
	}
}