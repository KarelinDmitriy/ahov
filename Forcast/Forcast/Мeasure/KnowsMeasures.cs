using System.Collections.Generic;

namespace Forcast.Мeasure
{
	public static class KnowsMeasures
	{
		public static MeasureType M()
		{
			return new MeasureType(new Dictionary<string, int>()
			{
				{ "m", 1}
			});
		}

		public static MeasureType S()
		{
			return new MeasureType(new Dictionary<string, int>
			{
				{"s", 1 }
			});
		}
	}
}