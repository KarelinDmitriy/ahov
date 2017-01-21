using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Forcast.Matters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace ForcastTest
{
	[TestClass]
	public class Parse
	{
		const string input = @"D:\Маг 785\Дисертация Телегина\Matters.csv";
		const string output = @"D:\Маг 785\Дисертация Телегина\Matters.json";

		[TestMethod]
		public void ReadFile()
		{
			var text = File.ReadAllText(output);
			var info = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MatterData>>(text);
			Console.WriteLine(info.First().Name);
		}

		[TestMethod]
		public void ParseFile()
		{
			
			var data = File.ReadAllLines(input, Encoding.GetEncoding(1251));
			var oData = data.Skip(1).Select(x =>
			{
				var parts = x.Split(';').Select(y => y.Trim()).ToList();
				var matter = new MatterData
				{
					Name = parts[0],
					First =
					{
						Sm = parts[1].Split('/').First().ToDouble(),
						Square_Sm = parts[1].Split('/').Last().ToDouble(),
						Mid = parts[2].Split('/').First().ToDouble(),
						Square_Mid = parts[2].Split('/').Last().ToDouble(),
						Pr = parts[3].Split('/').First().ToDouble(),
						Square_Pr = parts[3].Split('/').Last().ToDouble(),
						TempKoef_m40 = parts[16].Split('/').First().ToDouble(),
						TempKoef_0 = parts[17].Split('/').First().ToDouble(),
						TempKoef_20 = parts[18].Split('/').First().ToDouble(),
						TempKoef_40 = parts[19].Split('/').First().ToDouble()
					},
					Second =
					{
						Sm = parts[4].Split('/').First().ToDouble(),
						Square_Sm = parts[4].Split('/').Last().ToDouble(),
						Mid = parts[5].Split('/').First().ToDouble(),
						Square_Mid = parts[5].Split('/').Last().ToDouble(),
						Pr = parts[6].Split('/').First().ToDouble(),
						Square_Pr = parts[6].Split('/').Last().ToDouble(),
						TempKoef_m40 = parts[16].Split('/').Last().ToDouble(),
						TempKoef_0 = parts[17].Split('/').Last().ToDouble(),
						TempKoef_20 = parts[18].Split('/').Last().ToDouble(),
						TempKoef_40 = parts[19].Split('/').Last().ToDouble()
					},
					Code = parts[7],
					Type = parts[8],
					MoleculerMass = parts[9].ToDouble(),
					Density = parts[10].ToDouble(),
					Temperature = parts[11].ToDouble(),
					BoilingHeat = parts[12].ToDouble(),
					SpecificHeat = parts[13].ToDouble(),
					CrashTemperature = parts[14].ToDouble(),
					VaporSpeed = parts[15].ToDouble(),
					ToksiKoef_A_sm = parts[20].Split('/').First().ToDouble(),
					ToksiKoef_A_pr = parts[20].Split('/').Last().ToDouble(),
					ToksiKoef_B_sm = parts[21].Split('/').First().ToDouble(),
					ToksiKoef_B_pr = parts[21].Split('/').Last().ToDouble()
				};
				return matter;
			}).ToList();
			var result = Newtonsoft.Json.JsonConvert.SerializeObject(oData, Formatting.Indented);
			File.WriteAllText(output, result, Encoding.UTF8);
		}
	}

	public static class Temp
	{
		public static double ToDouble(this string s)
		{
			Console.WriteLine(s);
			return string.IsNullOrEmpty(s) ? 0 : Convert.ToDouble(s);
		}
	}
}