using System.Collections.Generic;
using System.ComponentModel;

namespace AhovRepository
{
	public class WebUser
	{
		[DisplayName("Логин")]
		public string Login { get; set; }

		[DisplayName("E-mail")]
		public string Email { get; set; }
		public int Role { get; set; }
		public int Id { get; set; }
	}

	public class WebCity
	{
		public WebCity() { CityTypes = new List<CityType>(); }

		public int Id { get; set; }

		[DisplayName("Название")]
		public string Name { get; set; }

		[DisplayName("Длинна")]
		public double Lenght { get; set; }

		[DisplayName("Ширина")]
		public double Width { get; set; }

		[DisplayName("Численность населения")]
		public int Population { get; set; }

		[DisplayName("Процент детей")]
		public int ChildPrecent { get; set; }

		[DisplayName("Доля занимающих укрытие")]
		public double Au { get; set; }
		[DisplayName("Доля занимающих укрытие (дети)")]
		public double AuChild { get; set; }

		[DisplayName("Для выходящих с зараженной местности")]
		public double Aw { get; set; }
		[DisplayName("Для выходящих с зараженной местности (дети)")]
		public double AwChild { get; set; }

		[DisplayName("Для применяющих антидоты")]
		public double Aa { get; set; }
		[DisplayName("Для применяющих антидоты (дети)")]
		public double AaChild { get; set; }

		[DisplayName("Доля применяющих СИЗ")]
		public double Apr { get; set; }
		[DisplayName("Доля применяющих СИЗ (дети)")]
		public double AprChild { get; set; }

		public List<CityType> CityTypes { get; set; }
	}

	public class CityType
	{
		public int Id { get; set; }
		public int CityId { get; set; }

		[DisplayName("Тип сооружения")]
		public string Name { get; set; }

		[DisplayName("Коэффициент проникнования")]
		public double Kp { get; set; }

		[DisplayName("Доля населения")]
		public double Ay { get; set; }
	}
}