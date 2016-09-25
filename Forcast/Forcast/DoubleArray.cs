using System;
using System.Collections.Generic;

namespace Forcast
{
	public class DoubleArray
	{
		private readonly double[,] _data;
		private readonly int x;
		private readonly int y;

		public DoubleArray() 
			: this(5,2)
		{ }

		public DoubleArray(int x, int y)
		{
			this.x = x;
			this.y = y;
			_data = new double[x,y];
		}

		public DoubleArray Select(Func<double, double> transform)
		{
			var result = new DoubleArray(x, y);
			foreach (var index in Index.GenerateIndices(x,y))
			{
				result[index] = transform(this[index]);
			}
			return result;
		}

		public DoubleArray Select(Func<double, Index, double> transform)
		{
			var result = new DoubleArray(x,y);
			foreach (var index in Index.GenerateIndices(x,y))
			{
				result[index] = transform(this[index], index);
			}
			return result;
		};

		public double this[int x, int y]
		{
			get { return _data[x, y]; }
			set { _data[x, y] = value; }
		}

		public double this[Index i]
		{
			get { return _data[i.X, i.Y]; }
			set { _data[i.X, i.Y] = value; }
		}
	}

	public class Index
	{
		public int X { get; set; }
		public int Y { get; set; }

		public static IEnumerable<Index> GenerateIndices(int x, int y)
		{
			for (int i=0; i<x; i++)
				for (int j=0; j<y; j++)
					yield return new Index {X = i, Y = j};
		}
	}
}