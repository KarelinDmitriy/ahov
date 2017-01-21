using System;
using System.Linq;
using Forcast;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ForcastTest
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			var forecast = new Forcast.Forcast(TestData.GetBarrels(), TestData.GetStorageData(), TestData.GetActiveData());
			var fullForecastResult = forecast.DoForcast();
			PrintResult(fullForecastResult);
			
		}

		[TestMethod]
		public void Test2()
		{
			var forecast = new Forcast.Forcast(TestData.GetBarrels().Take(1), TestData.GetStorageData2(), TestData.GetActiveData2());
			var fullForecastResult = forecast.DoForcast();
			PrintResult(fullForecastResult);
		}

		private static void PrintResult(Forcast.ForecastResults.FullForecastResult fullForecastResult)
		{
			Print(fullForecastResult.Go1, "Глубина распространения первичного облака", "м");
			Print(fullForecastResult.Go2, "Глубина распространения вторичного облака", "м");
			Print(fullForecastResult.So1, "Площадь распространения первичного облака", "м2");
			Print(fullForecastResult.So2, "Площадь распространения вторичного облака", "м2");
			Print(fullForecastResult.Nf, "Кол-во пораженных", "Чел");
			Print(fullForecastResult.Nf_san, "Санитарные потери", "Чел");
			Print(fullForecastResult.Nfs, "Кол-во пораженных с учетом возрастов", "Чел");
			Print(fullForecastResult.Nf_san, "Кол-во пораженных с учетом возрастов (санитарные)", "Чел");
			Print(fullForecastResult.Nos, "Пораженных среди персонала", "Чел");
			Print(fullForecastResult.Nof, "Пораженных среди персонала с учтом нормально распределения", "Чел");
			Print(new[] { fullForecastResult.Nof_San }, "Пораженных среди персонала санитарных", "Чел");
		}

		private static void Print(DoubleArray data, string caption, string v)
		{
			Console.WriteLine(caption);
			for (int i = 0; i < data.X; i++)
			{
				for (int j=0; j<data.Y; j++)
					Console.Write($"{data[i,j]:F1} {v}".PadLeft(24));
				Console.WriteLine();
			}
			Console.WriteLine();
		}

		private static void Print(double[] data, string caption, string v)
		{
			Console.WriteLine(caption);
			foreach (double t in data)
			{
				Console.WriteLine($"{t:F1} {v}".PadLeft(24));
			}
			Console.WriteLine();
		}
	}
}
