namespace Forcast.ForecastResults
{
	public class FullForecastResult
	{
		public DoubleArray Go1 { get; set; }
		public DoubleArray Go2 { get; set; }
		public DoubleArray So1 { get; set; }
		public DoubleArray So2 { get; set; }
		public DoubleArray Nps { get; set; }
		public DoubleArray Nf { get; set; }
		public double[] Nf_san { get; set; }
		public DoubleArray Nos { get; set; }
		public double[] Nof { get; set; }
		public double Nof_San { get; set; }
		public DoubleArray Nfs { get; set; }
		public double[] Nfs_San { get; set; }
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