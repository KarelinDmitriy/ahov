namespace Forcast.Matters
{
	public class MatterData
	{
		public MatterData()
		{
			First = new Cloud();
			Second = new Cloud();
		}

		public string Name { get; set; }
		public string Code { get; set; }
		/// <summary>
		/// Характиристики первичного облака 
		/// </summary>
		public Cloud First { get; set; }
		/// <summary>
		/// Характеристики вторичного облака
		/// </summary>
		public Cloud Second { get; set; }
		/// <summary>
		/// Тип вещества (a, b, c, d)
		/// </summary>
		public string Type { get; set; }
		/// <summary>
		/// Молекулярная масса, г/моль
		/// </summary>
		public double MoleculerMass { get; set; }
		/// <summary>
		/// плотность жидкости, т/м3
		/// </summary>
		public double Density { get; set; }
		/// <summary>
		/// Температура кипения
		/// </summary>
		public double Temperature { get; set; }
		/// <summary>
		/// Удельная теплота испарения, кДж/кг
		/// </summary>
		public double BoilingHeat { get; set; }
		/// <summary>
		/// Удельная теплоемкоть, кДж/(кг*С)
		/// </summary>
		public double SpecificHeat { get; set; }
		/// <summary>
		/// Температура разрушения
		/// </summary>
		public double CrashTemperature { get; set; }
		/// <summary>
		/// Скорость испарения (кг*с/м3)
		/// </summary>
		public double VaporSpeed { get; set; }
		/// <summary>
		/// Токсикологическая характеристика А (для смертельных)
		/// </summary>
		public double ToksiKoef_A_sm { get; set; }
		/// <summary>
		/// Токсикологическая характеристика Б (для смертельных)
		/// </summary>
		public double ToksiKoef_B_sm { get; set; }
		/// <summary>
		/// Токсикологическая характеристика А (для пороговых)
		/// </summary>
		public double ToksiKoef_A_pr { get; set; }
		/// <summary>
		/// Токсикологическая характеристика Б (для пороговых)
		/// </summary>
		public double ToksiKoef_B_pr { get; set; }
	}

	public class Cloud
	{
		/// <summary>
		/// Смертельное, км
		/// </summary>
		public double Sm { get; set; }
		/// <summary>
		/// Среднее, км
		/// </summary>
		public double Mid { get; set; }
		/// <summary>
		/// Пороговые, км
		/// </summary>
		public double Pr { get; set; }
		/// <summary>
		/// Площадь смертельного, км2
		/// </summary>
		public double Square_Sm { get; set; }
		/// <summary>
		/// Площадь среднее, км2
		/// </summary>
		public double Square_Mid { get; set; }
		/// <summary>
		/// Площадь пороговых, км2
		/// </summary>
		public double Square_Pr { get; set; }

		/// <summary>
		/// Температураный коэффицент при -40
		/// </summary>
		public double TempKoef_m40 { get; set; }
		/// <summary>
		/// Температураный коэффицент при 0
		/// </summary>
		public double TempKoef_0 { get; set; }
		/// <summary>
		/// Температураный коэффицент при 20
		/// </summary>
		public double TempKoef_20 { get; set; }
		/// <summary>
		/// Температураный коэффицент при 40
		/// </summary>
		public double TempKoef_40 { get; set; }
	}
}