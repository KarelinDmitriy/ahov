namespace Forcast
{
	public class ActiveData
	{
		/// <summary>
		/// Скорость ветра
		/// </summary>
		public double U { get; set; }
		/// <summary>
		/// Температура воздуха
		/// </summary>
		public double Tcw { get; set; }

		/// <summary>
		/// Время после аварии
		/// </summary>
		public double T { get; set; }

		/// <summary>
		/// Время оповещения населения 
		/// </summary>
		public double[] Tn { get; set; }

		/// <summary>
		/// Рассояние между центром пораженного объета и осью распространения ветра
		/// </summary>
		public double q { set; get; }

		/// <summary>
		/// вертикальная устойчивость воздуха
		/// </summary>
		public Table_3_3 AirVerticalStable { get; set; }

		public double Ku9 { get; set; } //Как его получать? 

		public double Ku2 { get; set; }
	}
}