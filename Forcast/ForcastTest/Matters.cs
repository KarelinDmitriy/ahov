using Forcast;

namespace ForcastTest
{
	public static class Matters
	{
		public static HMatter Azontay_Kislota() =>
			new HMatter
			{
				Cv = 1.32,
				E = 0.003,
				I = 481,
				M = 63,
				Pg = 1.531,
				Tcg = 20,
				Tck = 83,
				ToksiAndTempture = new ToksiAndTempture
				{
					Ap = 252,
					Asm = 3500,
					Bp = 1,
					Bsm = 1,
					M40pr = 0,
					M40sc = 0,
					P0pr = 0.7,
					P0sc = 0.8,
					P20pr = 1,
					P20sc = 1,
					P40pr = 1.3,
					P40sc = 1.7
				}
			};

		public static HMatter Brom() =>
			new HMatter
			{
				Pg = 3.102,
				M = 159.8,
				I = 59,
				E = 0.0137,
				Tcg = 20,
				Tck =59,
				Cv = 1,
				ToksiAndTempture = new ToksiAndTempture
				{
					P20pr = 1,
					P20sc = 1,
					M40sc = 1.8,
					P40pr = 1.4,
					P0sc = 0.2,
					P0pr = 0.1,
					P40sc = 1.4,
					M40pr = 0,
					Bsm = 0.66,
					Asm = 820,
					Ap = 120,
					Bp = 0.66
				}
			};

		public static HMatter Iprit() =>
			new HMatter
			{
				M = 159,
				I = 385,
				E = 0.000013,
				Tcg = 20,
				Tck = 217,
				Cv = 1.5,
				Pg = 1.274,
				ToksiAndTempture = new ToksiAndTempture
				{
					Bsm = 0.5,
					Asm = 130,
					Ap = 2.7,
					Bp = 0.5,
					M40pr = 0,
					P0pr = 0,
					P20pr = 1,
					P40pr = 2,
					M40sc = 0,
					P0sc = 0.1,
					P20sc = 1,
					P40sc = 1.2
				}
			};
	}
}