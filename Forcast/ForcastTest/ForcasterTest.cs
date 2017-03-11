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
	public class ForcasterTest
	{
		[TestMethod]
		public void FirstTest()
		{
			var cloudCalculator = new CloudCalculator(DataForFirstTest.GetBarrelsV2(), 
				DataForFirstTest.GetStorageData(), 
				DataForFirstTest.GetActiveData());
			var forcaster = new Forcaster(cloudCalculator);
			forcaster.Run();
			Console.WriteLine($"Вещество: {DataForFirstTest.GetBarrelsV2().First().Matter.Data.Name}");
			Print(cloudCalculator.iv.Qpa1, "Поражающий параметер первичного облака", "ед");
			Print(cloudCalculator.iv.Ga1, "Глубина распростронения (город) первичного облака", "м");
			Print(cloudCalculator.iv.Gam1, "Глубина распростронения первичного облака", "м");
			Print(cloudCalculator.iv.Sa1, "Площадь распространения (город) первичного облака", "м2");
			Print(cloudCalculator.iv.Sam1, "Площадь распространения первичного облака", "м2");
			var matter = DataForFirstTest.GetBarrelsV2().First().Matter.Data;
			Print(cloudCalculator.iv.Qpa2, "Поражающий параметер вторичного облака", "ед");
			Print(new[] { matter.Second.Sm, matter.Second.Mid, matter.Second.Pr }, "Данные по веществу");
			Print(cloudCalculator.iv.Ga2, "Глубина распростронения (город) вторичного облака", "м");
			Print(cloudCalculator.iv.Gam2, "Глубина распростронения вторичного облака", "м");
			Print(cloudCalculator.iv.Gao2, "Общая глубина распротраненяи вторичного облака", "м");
			Print(new[] { matter.Second.Square_Sm, matter.Second.Square_Mid, matter.Second.Square_Pr }, "Данные по веществу");
			Print(cloudCalculator.iv.Sa2, "Площадь распространения (город) вторичного облака", "м2");
			Print(cloudCalculator.iv.Sam2, "Площадь распространения вторичного облака", "м2");
			Print(cloudCalculator.iv.Sao2, "Общая площадь распростронения вторичного облака", "м2");

			Print(cloudCalculator.iv.Gas, "Общая глубина распротранения", "м");
			Print(cloudCalculator.iv.Sas, "Общая площадь распространения", "м2");

			Print(cloudCalculator.iv.Gas, "Приведенная глубина распротранения", "м");
			Print(cloudCalculator.iv.Sas, "Приведенная площадь распространения", "м2");

			Print(cloudCalculator.iv.Sap, "Непонятно что 1", "c?");
			Print(cloudCalculator.iv.Saw, "Непонятно что 2", "c?");
			Print(cloudCalculator.iv.App, "Непонятно что 3", "c?");
		}

		private static void Print(DoubleArray data, string caption, string v)
		{
			Console.WriteLine(caption);
			for (var i = 0; i < data.X; i++)
			{
				for (var j = 0; j < data.Y; j++)
					Console.Write($"{data[i, j]:F4} {v}".PadLeft(20));
				Console.WriteLine();
			}
			Console.WriteLine();
		}

		private static void Print(double[] data, string caption)
		{
			Console.WriteLine(caption);
			for (int i = 0; i < data.Length; i++)
			{
				Console.Write($"{data[i]:F5}".PadLeft(10));
			}
			Console.WriteLine();
		}
	}

	public static class DataForFirstTest
	{
		public static IEnumerable<BarrelV2> GetBarrelsV2()
		{
			var text = File.ReadAllText(@"D:\Маг 785\Дисертация Телегина\Matters.json");
			var info = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MatterData>>(text);
			var matterData = info.Skip(3).First();
			yield return new BarrelV2
			{
				Draining = Draining.Vp1,
				H = 2,
				Q = 50,
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
				Kf = 1,
				Rz = 200,
				Цх = 7000,
				Gdl = 0,
				Gl = 0,
				W = 0,
				Цу = 7000,
				apr = new[] {0.5d, 0.5d},
				aa = new[] { 0.5d, 0.5d },
				au = new[] { 0.5d, 0.5d },
				aw = new[] { 0.5d, 0.5d },
				b = new[] { 0.5d, 0.5d },
				ba = new[] { 0.5d, 0.5d },
				bu = new[] { 0.5d, 0.5d },
				bw = new[] { 0.5d, 0.5d },
				
			};
		}

		public static ActiveData GetActiveData()
		{
			return new ActiveData
			{
				Ku2 = 1,
				Ku9 = 1,
				AirVerticalStable = new Table_3_3(StateType.F),
				U = 1,
				Tcw = 20,
				T = 300,
				To = 60,
				q = 3,
				Tn = new[] {350, 350d}
			};
		}
	}
	
}