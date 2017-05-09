using System.IO;
using Forcast.Report;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ForcastTest
{
	[TestClass]
	public class ReportTest
	{
		[TestMethod]
		public void GenerateReportTest()
		{
			var generator = new ReportGenerator();
			File.WriteAllBytes(@"D:\Маг 785\Дисертация Телегина\Пояснительная записка\report.pdf", generator.GenerateReport(null));
		}
	}
}