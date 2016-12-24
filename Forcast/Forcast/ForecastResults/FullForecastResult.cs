namespace Forcast.ForecastResults
{
	public class FullForecastResult
	{
		/// <summary>
		/// Глубина распространения первичного облака
		/// </summary>
		public DoubleArray Go1 { get; set; }
		/// <summary>
		/// Глубина распространения вторичного облака
		/// </summary>
		public DoubleArray Go2 { get; set; }
		/// <summary>
		/// Площадь распространения первичного облака
		/// </summary>
		public DoubleArray So1 { get; set; }
		/// <summary>
		/// Площадь распространения вторичного облака
		/// </summary>
		public DoubleArray So2 { get; set; }
		/// <summary>
		/// Общее кол-во пострадавщих
		/// </summary>
		public DoubleArray Nps { get; set; }
		/// <summary>
		/// Кол-во пораженных
		/// </summary>
		public DoubleArray Nf { get; set; }
		/// <summary>
		/// Санитарные потери
		/// </summary>
		public double[] Nf_san { get; set; }
		/// <summary>
		/// Пороженных среди персонала
		/// </summary>
		public double[] Nos { get; set; }
		/// <summary>
		/// кол-во пораженных среди персонала с учетом нормального распреденения 
		/// </summary>
		public double[] Nof { get; set; }
		/// <summary>
		/// кол-во пораженных среди персонала санитарные
		/// </summary>
		public double Nof_San { get; set; }
		/// <summary>
		/// общее кол-во пораженных с учетом возрастов
		/// </summary>
		public DoubleArray Nfs { get; set; }
		/// <summary>
		/// общее кол-во пораженных с учетом возрастов санитарные
		/// </summary>
		public double[] Nfs_San { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public double[] Ts { get; set; }
	}

	internal class ForecastResultForDef
	{
		public DoubleArray Npa { get; set; }
		public double[] Nopa { get; set; }
		public DoubleArray Gao1 { get; set; }
		public DoubleArray Sao1 { get; set; }
		public DoubleArray Gao2 { get; set; }
		public DoubleArray Sao2 { get; set; }
	}

	internal class ForecastResultWithoutDef
	{
		public DoubleArray Np { get; set; }
		public double[] Nop { get; set; }
		public DoubleArray Go1 { get; set; }
		public DoubleArray So1 { get; set; }
		public DoubleArray Go2 { get; set; }
		public DoubleArray So2 { get; set; }
	}
}