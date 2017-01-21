using System;
using System.Collections.Generic;
using System.Linq;
using Forcast.Matters;

namespace Forcast.V2
{
	public class Forcaster
	{
		private readonly CloudCalculator _cloudCalculator;

		public Forcaster(CloudCalculator cloudCalculator)
		{
			_cloudCalculator = cloudCalculator;
		}
	}

	public class CloudCalculator
	{
		private readonly IEnumerable<BarrelV2> _barrels;
		private readonly StorageData _storageData;
		private readonly ActiveData _activeData;

		public CloudCalculator(IEnumerable<BarrelV2> barrels,
			StorageData storageData,
			ActiveData activeData)
		{
			_barrels = barrels;
			_storageData = storageData;
			_activeData = activeData;
		}

		public void DoAction()
		{
			//TODO: расчитать нормально
			var kp = 1;
			var qpa1 = Qpa1(kp);

		}

		private DoubleArray Qpa1(double kp)
		{
			var result = new DoubleArray();
			var koef = kp / Math.Pow(_activeData.U, 0.7) * _storageData.Kf;
			foreach (var i in Index.GenerateIndices(5,2))
			{
				var s = _barrels.Sum(x => x.Q1/(x.Matter.ToksiDosesDeaf()[i]*Constants.Kad[i.X]));
				result[i] = koef*s;
			}
			return result;
		}

		private DoubleArray Qpa2(double kp)
		{
			var result = new DoubleArray();
			var koef = 2826*1000*kp*_storageData.Kf/Math.Pow(_activeData.U, 0.7);
			foreach (var i in Index.GenerateIndices(5,2))
			{
				
			}
			return result;
		}
	}

	public class BarrelV2
	{
		public Matter Matter { get; set; }
		public double Q { get; set; }
		public MatterSaveType SaveType { get; set; }

		public double Q1
		{
			get
			{
				if (SaveType == MatterSaveType.Cx1)
					return 0.02*Q;
				if (SaveType == MatterSaveType.Cx2)
				{
					var kb1 = Matter.Data.SpecificHeat*(Matter.Data.CrashTemperature - Matter.Data.Temperature)/Matter.Data.BoilingHeat;
					var kb2 = 0.02* Math.Pow(20/Matter.Data.Temperature,3);
					if (kb1 > 1)
						kb1 = 1;
					if (kb1 < 0)
						kb1 = 0;
					return Q*(kb1 + kb2);
				}
				return Q*(20/Matter.Data.Temperature);
			}
		}

		public double Q2 => Q - Q1;

		public double D
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}