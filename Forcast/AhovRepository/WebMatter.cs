using System.ComponentModel;

namespace AhovRepository
{
	public class WebMatter
	{
		public int Id { get; set; }

		[DisplayName("Название")]
		public string Name { get; set; }

		[DisplayName("Удельная теплоемкость")]
		public double Cv { get; set; }

		[DisplayName("Тепмература до аварии")]
		public double Tcg { get; set; }

		[DisplayName("Температура кипения")]
		public double Tck { get; set; }

		[DisplayName("Удельная теплота испарения")]
		public double I { get; set; }

		[DisplayName("Плотность жидкости")]
		public double Pg { get; set; }

		[DisplayName("Молярная масса")]
		public double M { get; set; }

		[DisplayName("Скорость испарения")]
		public double E { get; set; }

		[DisplayName("Токсикологическая хар-ка А (смертельные)")]
		public double Asm { get; set; }

		[DisplayName("Токсикологическая хар-ка B (смертельные)")]
		public double Bsm { get; set; }

		[DisplayName("Токсикологическая хар-ка А (пороговые)")]
		public double Ap { get; set; }

		[DisplayName("Токсикологическая хар-ка B (пороговые)")]
		public double Bp { get; set; }
	}
}