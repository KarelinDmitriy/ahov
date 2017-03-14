using System;
using System.Collections.Generic;
using System.Linq;

namespace Forcast.V2
{
	public class CloudCalculatorForAntidots : CloudCalculator
	{
		public CloudCalculatorForAntidots(IEnumerable<BarrelV2> barrels, StorageData storageData, ActiveData activeData) : base(barrels, storageData, activeData)
		{
		}

		protected override void NpaNopa()
		{
			var npa = new DoubleArray();
			var nopa = new DoubleArray();
			var combo = iv.App.Select((x, i) => x * iv.Saw[i]);
			foreach (var i in Index.GenerateIndices(5, 2))
			{
				npa[i] = StorageData.N * StorageData.a[i.Y] * StorageData.aa[i.Y]
				         * (DoubleArray.SafeSubstruct(iv.Sap, i) + DoubleArray.SafeSubstruct(combo, i))
				         / (StorageData.Öõ * iv.Cyp[i]);
				var soapDelta = i.X == 0 ? iv.Soap[i.X] : iv.Soap[i.X] - iv.Soap[i.X - 1];
				nopa[i] = StorageData.No * StorageData.aao * soapDelta
				          / (Math.PI * StorageData.Ro * StorageData.Ro);
			}
			iv.Npa = npa;
			iv.Nopa = nopa;
		}

		protected override void SapSawApp()
		{
			new SawSapCalculator(StorageData, ActiveData, iv, false).Calculate();
		}

		protected override void Qpa1(double kp)
		{
			// formula number 12
			var result = new DoubleArray();
			var koef = (kp / Math.Pow(ActiveData.U, 0.7)) * StorageData.Kf;
			foreach (var i in Index.GenerateIndices(5, 2))
			{
				var s = Barrels.Sum(x => x.Q1 / (x.Matter.ToksiDosesDeaf()[i] * Constants.Kad[i.X]));
				result[i] = koef * s;
			}
			iv.Qpa1 = result;
		}

		protected override void Qpa2(double kp)
		{
			var result = new DoubleArray();
			var koef = 2826 * 1000 * kp * StorageData.Kf / Math.Pow(ActiveData.U, 0.7);
			Func<BarrelV2, double> er =
				b => 1e-6 * Math.Sqrt(b.Matter.Data.MoleculerMass) * Math.Pow(10, 2.76 - 0.019 * b.Matter.Data.Temperature + 0.024 * ActiveData.Tcw) * (5.4 + 2.7 * ActiveData.U);
			foreach (var i in Index.GenerateIndices(5, 2))
			{
				result[i] = koef * Barrels.Sum(x => x.D * x.D * er(x) * 4 / (x.Matter.ToksiDosesPorog()[i] * Constants.Kad[i.X]));
			}
			iv.Qpa2 = result;
		}
	}
}