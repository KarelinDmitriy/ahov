using System;

namespace Forcast.Matters
{
	public class Matter
	{
		public MatterData Data { get; set; }

		public double Ku9Pr(double T)
		{
			if (T < 20)
				return Data.First.TempKoef_m40;
			if (T >= 20 && T < 10)
				return Data.First.TempKoef_0;
			if (T >= 10 && T < 30)
				return Data.First.TempKoef_20;
			if (T >= 30)
				return Data.First.TempKoef_40;
			throw new InvalidOperationException("ошибка с double?");
		}

		public double Ku9Sc(double T)
		{
			if (T < 20)
				return Data.Second.TempKoef_m40;
			if (T >= 20 && T < 10)
				return Data.Second.TempKoef_0;
			if (T >= 10 && T < 30)
				return Data.Second.TempKoef_20;
			if (T >= 30)
				return Data.Second.TempKoef_40;
			throw new InvalidOperationException("ошибка с double?");
		}

		public DoubleArray ToksiDosesDeaf()
		{
			var result = new DoubleArray
			{
				[0, 0] = Data.ToksiKoef_A_sm * Math.Pow(0.5, Data.ToksiKoef_B_sm),
				[4, 0] = Data.ToksiKoef_A_pr * Math.Pow(0.5, Data.ToksiKoef_B_pr),
			};
			result[1, 0] = 0.624 * result[0, 0] + 0.414 * result[4, 0];
			result[2, 0] = 0.392 * result[0, 0] + 0.632 * result[4, 0];
			result[3, 0] = 0.162 * result[0, 0] + 0.850 * result[4, 0];
			for (var i = 0; i < 5; i++)
				result[i, 1] = 0.2*result[i, 0];
			return result;
		}

		public DoubleArray ToksiDosesPorog()
		{
			var result = new DoubleArray()
			{
				[0, 0] = Data.ToksiKoef_A_sm * Math.Pow(4, Data.ToksiKoef_B_sm),
				[4, 0] = Data.ToksiKoef_A_pr * Math.Pow(4, Data.ToksiKoef_B_pr)
			};
			result[1, 0] = 0.624 * result[0, 0] + 0.414 * result[4, 0];
			result[2, 0] = 0.392 * result[0, 0] + 0.632 * result[4, 0];
			result[3, 0] = 0.162 * result[0, 0] + 0.850 * result[4, 0];
			for (var i = 0; i < 5; i++)
				result[i, 1] = 0.2 * result[i, 0];
			return result;
		}
	}
}