using Forcast.Мeasure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ForcastTest
{
	[TestClass]
	public class MeasureTest
	{
		[TestMethod]
		public void TestAdd()
		{
			var m1 = new MeasureValue(5, KnowsMeasures.M());
			var m2 = new MeasureValue(10, KnowsMeasures.M());
			var m3 = m1 + m2;
			Assert.AreEqual(m3.Value, 15);
			Assert.AreEqual("(м)", m3.Measure);
		}

		[TestMethod]
		public void TestSub()
		{
			var m1 = new MeasureValue(5, KnowsMeasures.M());
			var m2 = new MeasureValue(10, KnowsMeasures.M());
			var m3 = m1 - m2;
			Assert.AreEqual(m3.Value, -5);
			Assert.AreEqual("(м)", m3.Measure);
		}

		[TestMethod]
		public void TestMult()
		{
			var m1 = new MeasureValue(5, KnowsMeasures.M());
			var m2 = new MeasureValue(10, KnowsMeasures.M());
			var m3 = m1 * m2;
			Assert.AreEqual(m3.Value, 50);
			Assert.AreEqual("(м^2)", m3.Measure);
		}

		[TestMethod]
		public void TestDiv()
		{
			var m1 = new MeasureValue(5, KnowsMeasures.M());
			var m2 = new MeasureValue(10, KnowsMeasures.M());
			var m3 = m1 / m2;
			Assert.AreEqual(m3.Value, 0.5);
			Assert.AreEqual("", m3.Measure);
		}
	}
}