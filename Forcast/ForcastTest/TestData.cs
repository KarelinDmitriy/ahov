using System.Collections.Generic;
using Forcast;

namespace ForcastTest
{
	internal static class TestData
	{
		public static IEnumerable<Barrel> GetBarrels()
		{
			yield return new Barrel
			{
				MatterSaveType = MatterSaveType.Cx1,
				Matter = Matters.Azontay_Kislota(),
				Q = 100,
				H = 2, 
				Draining = Draining.Vp1
			};
			yield return new Barrel
			{
				Matter = Matters.Brom(),
				Draining = Draining.Vp2,
				MatterSaveType = MatterSaveType.Cx2,
				H = 1.5,
				Q = 120,
			};
			yield return new Barrel
			{
				Matter = Matters.Iprit(),
				Draining = Draining.Vp1,
				H = 3,
				Q = 600,
				MatterSaveType = MatterSaveType.Cx3
			};
		}

		public static StorageData GetStorageData()
		{
			return new StorageData
			{
				Цу = 15000,
				Цх = 10000,
				N = 600000,
				Ro = 500,
				Rz = 1000,
				a = new [] {0.7, 0.3},
				W = 30,
				Kf = 0.8,
				aao = .8,
				Gdl = 2000,
				Gl = 1500,
				No = 2000,
				Top = 120,
				Tow = 300,
				aa = new[] {.3, .5},
				b = new[] {0.6, 0.5},
				apr = new[] {0.3, .5},
				au = new[] {0.2, 0.3},
				aw = new[] {0.3, 0.3},
				ba = new[] {0.015, 0.01},
				bw = new[] {0.001, .0007},
				bu = new [] {0,005, 0.003},
				QInside = new[]
				{
					new QInside
					{
						Ay = .3,
						Kp = 0.7
					},
					new QInside
					{
						Kp = 0.5,
						Ay = 0.2
					},
					new QInside
					{
						Kp = 0.2,
						Ay = 0.1
					},
					new QInside
					{
						Ay = 0.4,
						Kp = 0.5
					}
				}
			};
		}

		public static ActiveData GetActiveData()
		{
			return new ActiveData
			{
				To = 60,
				AirVerticalStable = new Table_3_3(StateType.B),
				T = 200,
				Tcw = 20,
				U = 1,
				Tn = new []{180d, 240},
				q = 0.2,
				Ku2 = 1,
				Ku9 = 1
			};
		}
	}
}