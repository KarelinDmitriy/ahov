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
				HMatter = Matters.Azontay_Kislota(),
				Q = 50,
				H = 2, 
				Draining = Draining.Vp1
			};
			//yield return new Barrel
			//{
			//	HMatter = Matters.Brom(),
			//	Draining = Draining.Vp2,
			//	MatterSaveType = MatterSaveType.Cx2,
			//	H = 1.5,
			//	Q = 30,
			//};
			//yield return new Barrel
			//{
			//	HMatter = Matters.Iprit(),
			//	Draining = Draining.Vp1,
			//	H = 3,
			//	Q = 40,
			//	MatterSaveType = MatterSaveType.Cx3
			//};
		}

		public static StorageData GetStorageData()
		{
			return new StorageData
			{
				Цу = 15000,
				Цх = 10000,
				Цx_p = 1200,
				Цу_p = 800,
				N = 600000,
				Ro = 1500,
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
				apr = new[] {0.3, .5},
				au = new[] {0.2, 0.3},
				aw = new[] {0.3, 0.3},
				b = new[] { 0.6, 0.5 },
				ba = new[] {0.015, 0.01},
				bw = new[] {0.001, .0007},
				bu = new [] {0,005, 0.003},
				QInside = new[]
				{
					new QInside
					{
						Ay = 1,
						Kp = 1
					},
					//new QInside
					//{
					//	Kp = 0.5,
					//	Ay = 0.2
					//},
					//new QInside
					//{
					//	Kp = 0.2,
					//	Ay = 0.1
					//},
					//new QInside
					//{
					//	Ay = 0.4,
					//	Kp = 0.5
					//}
				}
			};
		}

		public static StorageData GetStorageData2()
		{
			return new StorageData
			{
				Rz = 1100,
				N = 50000,
				Цх = 2000,
				Цу = 1000,
				QInside = new[] {new QInside {Ay = 1, Kp = 0.6}, },
				Gdl = 1000,
				Gl = 1000,
				Kf = 0.6,
				No = 10000,
				Ro = 300,
				Top = 20,
				Tow = 200,
				W = 10,
				a = new[] {0.7, 0.3},
				aa = new [] {0.0, 0.0},
				aao = 1,
				apr = new[] {0.0, 0},
				au = new [] {0.2, 0.2},
				aw = new [] {0.3, 0.3},
				b = new[] { 0.6, 0.5 },
				ba = new[] { 0.015, 0.01 },
				bw = new[] { 0.001, .0007 },
				bu = new[] { 0, 005, 0.003 },
			};
		}

		public static ActiveData GetActiveData2()
		{
			return new ActiveData
			{
				T = 600,
				To = 300,
				U = 1,
				AirVerticalStable = new Table_3_3(StateType.A),
				Ku2 = 1,
				Ku9 = 1,
				Tcw = 20,
				Tn = new []{600d, 600d},
				q = 2
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