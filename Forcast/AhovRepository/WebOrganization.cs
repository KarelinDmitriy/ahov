using System.ComponentModel;

namespace AhovRepository
{
	public class WebOrganization
	{
		public int Id { get; set; }


		public int CityId { get; set; }

		[DisplayName("Название организации")]
		public string Name { get; set; }

		[DisplayName("Численность персонала")]
		public int PersonalCount { get; set; }

		[DisplayName("Коэффициент физической нагрузки")]
		public double Kf { get; set; }

		[DisplayName("Радиус объекта")]
		public double Ro { get; set; }

		[DisplayName("Радиус санитарной зоны")]
		public double Rz { get; set; }

		[DisplayName("Время одевания СИЗ")]
		public double Top { get; set; }

		[DisplayName("Время выхода")]
		public double Tow { get; set; }

		[DisplayName("Растояние до леса")]
		public double Gdl { get; set; }

		[DisplayName("Глубина леса")]
		public double Gl { get; set; }

		[DisplayName("Высота возвышенностей")]
		public double W { get; set; }

		[DisplayName("Доля применяющих антидоты")]
		public double Aao { get; set; }

		[DisplayName("Скорость выхода с зараженной зоны")]
		public double Bw { get; set; }

		[DisplayName("Скорость выхода (для детей)")]
		public double Bw_ch { get; set; }

		[DisplayName("Скорость занятий укрытий")]
		public double Bu { get; set; }

		[DisplayName("Скорость занятий укрытий (для детей)")]
		public double Bu_ch { get;set; }

		[DisplayName("Время принятия антидотов")]
		public double Ba { get; set; }

		[DisplayName("Время принятия антидотов (для детей)")]
		public double Ba_ch { get; set; }
	}
}