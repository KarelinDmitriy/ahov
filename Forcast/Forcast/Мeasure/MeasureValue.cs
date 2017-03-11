using System;

namespace Forcast.Мeasure
{
	public class MeasureValue
	{
		private readonly MeasureType ownMeasure;
		private readonly double value;

		public string Measure => ownMeasure.Value;
		public double Value => value;

		public MeasureValue(double value, MeasureType measure)
		{
			this.ownMeasure = measure;
			this.value = value;
		}

		public override string ToString()
		{
			return $"{value:F5} {ownMeasure.Value}";
		}

		public static MeasureValue operator +(MeasureValue v1, MeasureValue v2)
		{
			if (v1 == null || v2 == null)
				throw new ArgumentNullException();
			if (!v1.ownMeasure.Equals(v2.ownMeasure))
				throw new ArgumentException("Measure are not equals");
			return new MeasureValue(v1.value+v2.value, v1.ownMeasure);
		}

		public static MeasureValue operator -(MeasureValue v1, MeasureValue v2)
		{
			if (v1 == null || v2 == null)
				throw new ArgumentNullException();
			if (!v1.ownMeasure.Equals(v2.ownMeasure))
				throw new ArgumentException("Measure are not equals");
			return new MeasureValue(v1.value - v2.value, v1.ownMeasure);
		}

		public static MeasureValue operator *(MeasureValue v1, MeasureValue v2)
		{
			if (v1 == null || v2 == null)
				throw new ArgumentNullException();
			return new MeasureValue(v1.value*v2.value, v1.ownMeasure * v2.ownMeasure);
		}

		public static MeasureValue operator /(MeasureValue v1, MeasureValue v2)
		{
			if (v1 == null || v2 == null)
				throw new ArgumentNullException();
			return new MeasureValue(v1.value / v2.value, v1.ownMeasure / v2.ownMeasure);
		}
	}
}