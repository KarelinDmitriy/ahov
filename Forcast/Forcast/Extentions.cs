using System;

namespace Forcast
{
	public static class Extentions
	{
		public static bool MyEquals(this double a, double arg)
		{
			const double e = 10E-9;
			return Math.Abs(a - arg) < e;
		}
	}
}