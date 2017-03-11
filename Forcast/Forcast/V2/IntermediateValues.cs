namespace Forcast.V2
{
	public class IntermediateValues
	{
		/// <summary>
		/// Поражающий параметр первичного облака
		/// </summary>
		public DoubleArray Qpa1 { get; set; }
		/// <summary>
		/// Поражающий параметр вторичного облака
		/// </summary>
		public DoubleArray Qpa2 { get; set; }

		/// <summary>
		/// Глубина распространения первичного облака в городе
		/// </summary>
		public DoubleArray Ga1 { get; set; }
		/// <summary>
		/// Глубина распростронения первичного облака на открытой местности
		/// </summary>
		public DoubleArray Gam1 { get; set; }
		/// <summary>
		/// Площади распространения первичного облака в городе
		/// </summary>
		public DoubleArray Sa1 { get; set; }
		/// <summary>
		/// Площади распростроенения первичного облака на открытой месности
		/// </summary>
		public DoubleArray Sam1 { get; set; }

		/// <summary>
		/// Глубина распространения вторичного облака в городе
		/// </summary>
		public DoubleArray Ga2 { get; set; }
		/// <summary>
		/// Глубина распростронения вторичного облака на открытой местности
		/// </summary>
		public DoubleArray Gam2 { get; set; }
		/// <summary>
		/// Площади распространения вторичного облака в городе
		/// </summary>
		public DoubleArray Sa2 { get; set; }
		/// <summary>
		/// Площади распростроенения вторичного облака на открытой месности
		/// </summary>
		public DoubleArray Sam2 { get; set; }

		/// <summary>
		/// Общая глубина распространения первичного облака
		/// </summary>
		public DoubleArray Gao1 { get; set; }
		/// <summary>
		/// Общая глубина распространения вторичного облака
		/// </summary>
		public DoubleArray Gao2 { get; set; }
		/// <summary>
		/// Общая площадь распространения первичного облака
		/// </summary>
		public DoubleArray Sao1 { get; set; }
		/// <summary>
		/// Общая площадь распространения вторичного облака
		/// </summary>
		public DoubleArray Sao2 { get; set; }

		/// <summary>
		/// Общая глубиная распространения облака
		/// </summary>
		public DoubleArray Gas { get; set; }
		/// <summary>
		/// Общая площадь распространения облака
		/// </summary>
		public DoubleArray Sas { get; set; }

		/// <summary>
		/// Приведенная глубина распространения
		/// </summary>
		public DoubleArray Gau { get; set; }
		/// <summary>
		/// Приведенная площадь распространения
		/// </summary>
		public DoubleArray Sau { get; set; }

		public DoubleArray Sap { get; set; }
		public DoubleArray Saw { get; set; }
		public DoubleArray App { get; set; }

		/// <summary>
		/// Время незащещенности персонала 
		/// </summary>
		public double Ton { get; set; }

		public double[] Soap { get; set; }
	}
}