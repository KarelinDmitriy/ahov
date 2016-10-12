namespace Forcast.ForecastResults
{
	public class FullForecastResult
	{
	}

	internal class ForecastResultForDef
	{
		public DoubleArray Npa { get; set; }
		public double[] Nopa { get; set; }
	}

	internal class ForecastResultWithoutDef
	{
		public DoubleArray Np { get; set; }
		public double[] Nop { get; set; }
	}
}