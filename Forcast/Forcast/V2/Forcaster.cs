using System.Collections.Generic;

namespace Forcast.V2
{
	public class Forcaster
	{
		public readonly CloudCalculator CalculatorWithoutDef;
		public readonly CloudCalculator CalculatorWithDef;
		public Result Result { get; set; } = new Result();

		public Forcaster(IEnumerable<BarrelV2> barrels,
			StorageData storageData,
			ActiveData activeData)
		{
			CalculatorWithDef = new CloudCalculatorForAntidots(barrels, storageData, activeData);
			CalculatorWithoutDef = new CloudCalculatorWithoutAntidots(barrels, storageData, activeData);
		}

		public void Run()
		{
			CalculatorWithoutDef.DoAction();
			CalculatorWithDef.DoAction();
			Result.Nps = CalculatorWithDef.iv.Npa.Select((x, i) => x + CalculatorWithoutDef.iv.Npa[i]);
			Result.Nos = CalculatorWithDef.iv.Nopa.Select((x, i) => x + CalculatorWithoutDef.iv.Nopa[i]);
			Result.Nf = CalculateNf(Result.Nps);
			Result.Nfs = CalculateNf(Result.Nos);
		}

		private DoubleArray CalculateNf(DoubleArray nps)
		{
			var result = new DoubleArray();
			for (var i = 0; i < 2; i++)
			{
				result[0, i] = 0.6 * nps[0, i] + 0.1 * nps[1, i] + 0.05 * nps[2, i];
				result[1, i] = 0.25 * nps[0, i] + 0.5 * nps[1, i] + 0.1 * nps[2, i] + 0.05 * nps[3, i];
				result[2, i] = 0.1 * nps[0, i] + 0.25 * nps[1, i] + 0.5 * nps[2, i] + 0.1 * nps[3, i] + 0.05 * nps[4, i];
				result[3, i] = 0.05 * nps[0, i] + 0.1 * nps[1, i] + 0.25 * nps[2, i] + 0.5 * nps[3, i] + 0.1* nps[4, i];
				result[4,i] = 0.05 * nps[1, i] + 0.1 * nps[2, i] + 0.25 * nps[3, i] + 0.5 * nps[4, i];
			}
			return result;
		}
	}

	public class Result
	{
		/// <summary>
		/// Потери среди населения
		/// </summary>
		public DoubleArray Nps { get; set; }
		/// <summary>
		/// Потери среди персонала
		/// </summary>
		public DoubleArray Nos { get; set; }
		/// <summary>
		/// Потери среди населения (с учетом распредеения)
		/// </summary>
		public DoubleArray Nf { get; set; }
		/// <summary>
		/// Потери среди персонала (с учетом распредеения)
		/// </summary>
		public DoubleArray Nfs { get; set; }

		public double[] Nf_all => Nf.Zip((x, y) => x + y);

		public double[] Nfs_all => Nfs.Zip((x, y) => x + y);
	}
}