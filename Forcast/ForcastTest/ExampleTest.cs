using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Forcast;
using Forcast.Matters;
using Forcast.V2;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ForcastTest
{
	[TestClass]
	public class ExampleTest
	{
		[TestMethod]
		public void Example()
		{
			var forcaster = new Forcaster(DataForExample.GetBarrelsV2(),
				DataForExample.GetStorageData(),
				DataForExample.GetActiveData());
			forcaster.Run();
			Print(forcaster.Result.Nf_all, "Потери среди населения");
			Print(forcaster.Result.Nfs_all, "Потери среди персонала");
		}

		private static void Print(double[] data, string caption)
		{
			Console.WriteLine(caption);
			for (int i = 0; i < data.Length; i++)
			{
				Console.WriteLine($"{data[i]:F5}".PadLeft(10));
			}
			Console.WriteLine();
		}

		private static class DataForExample
		{
			public static IEnumerable<BarrelV2> GetBarrelsV2()
			{
				var text = File.ReadAllText(@"D:\Маг 785\Дисертация Телегина\Matters.json");
				var info = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MatterData>>(text);
				var matterData = info.First(x => x.Name == "Хлор");
				yield return new BarrelV2
				{
					Draining = Draining.Vp1,
					H = 0.8,
					Q = 100,
					SaveType = MatterSaveType.Cx1,
					Matter = new Matter()
					{
						Data = matterData
					}
				};
			}

			public static StorageData GetStorageData()
			{
				return new StorageData
				{
					Tow = 300,
					Top = 60,
					Kf = 0.6,
					Rz = 1100,
					Ro = 1000,
					Цх = 7000,
					Gdl = 0,
					Gl = 0,
					W = 0,
					Цу = 7000,
					apr = new[] { 0.5d, 0.5d },
					aa = new[] { 0.5d, 0.5d },
					au = new[] { 0.5d, 0.5d },
					aw = new[] { 0.5d, 0.5d },
					b = new[] { 0.5d, 0.5d },
					ba = new[] { 0.5d, 0.5d },
					bu = new[] { 0.5d, 0.5d },
					bw = new[] { 0.5d, 0.5d },
					a = new[] { 0.8d, 0.2d },
					No = 10000,
					N = 300000,
					aao = 0.9,
					Цу_p = 2000,
					Цx_p = 1000,
				};
			}

			public static ActiveData GetActiveData()
			{
				return new ActiveData
				{
					Ku2 = 1,
					Ku9 = 1,
					AirVerticalStable = new Table_3_3(StateType.F),
					U = 2,
					Tcw = 20,
					T = 300,
					To = 300,
					q = 3,
					Tn = new[] { 350, 350d }
				};
			}
		}
	}
}