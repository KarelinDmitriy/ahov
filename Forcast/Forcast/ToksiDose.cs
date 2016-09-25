using System;

namespace Forcast
{
	public class ToksiDose //доксидоза; детские дозы получаем через расширение
	{
		public ToksiDose(ToksiAndTempture toksiAndTempture)
		{
			Dsm1 = toksiAndTempture.Asm * Math.Pow(0.5, toksiAndTempture.Bsm);
			Dsm2 = toksiAndTempture.Asm * Math.Pow(4, toksiAndTempture.Bsm);
			Dp1 = toksiAndTempture.Ap * Math.Pow(0.5, toksiAndTempture.Bp);
			Dp2 = toksiAndTempture.Ap * Math.Pow(4, toksiAndTempture.Bp);
			PrimaryArray = new DoubleArray()
			{
				[0, 0] = Dsm1,
				[1, 0] = Dp1,
				[2, 0] = Dt1,
				[3, 0] = Ds1,
				[4, 0] = Dl1,
				[0, 1] = Dsm1.Child(),
				[1, 1] = Dp1.Child(),
				[2, 1] = Dt1.Child(),
				[3, 1] = Ds1.Child(),
				[4, 1] = Dl1.Child()
			};
			SecondaryArray = new DoubleArray
			{
				[0, 0] = Dsm2,
				[1, 0] = Dp2,
				[2, 0] = Dt2,
				[3, 0] = Ds2,
				[4, 0] = Dl2,
				[0, 1] = Dsm2.Child(),
				[1, 1] = Dp2.Child(),
				[2, 1] = Dt2.Child(),
				[3, 1] = Ds2.Child(),
				[4, 1] = Dl2.Child()
			};

		}

		public DoubleArray PrimaryArray { get; }
		public DoubleArray SecondaryArray { get; }

		public double Dsm1 { get; set; } //смертельная доза от первичного
		public double Dsm2 { get; set; } //смертельная от вторичного
		public double Dp1 { get; set; } //пороговая от первичного
		public double Dp2 { get; set; } //пороговая от вторичного
		public double Dt1 => Dsm1 * 0.624 + Dp1 * 0.414; //тяжелая для первичного
		public double Dt2 => Dsm2 * 0.624 + Dp2 * 0.414; //тяжелая для вторичного
		public double Ds1 => Dsm1 * 0.392 + Dp1 * 0.632; //средняя для первичного
		public double Ds2 => Dsm2 * 0.392 + Dp2 * 0.632; //средняя для вторичного
		public double Dl1 => Dsm1 * 0.162 + Dp1 * 0.85; //легкая для первичного
		public double Dl2 => Dsm2 * 0.162 + Dp2 * 0.85; //легкая для вторичного
	}
}