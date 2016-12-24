using System;
using System.Collections.Generic;

namespace Forcast
{
	public class DoubleArray
	{
		private readonly double[,] _data;
		private readonly int _x;
		private readonly int _y;

		public int X => _x;
		public int Y => _y;

		public DoubleArray() 
			: this(5,2)
		{ }

		public DoubleArray(int x, int y)
		{
			this._x = x;
			this._y = y;
			_data = new double[x,y];
		}

		public DoubleArray(IReadOnlyList<double> init, int y = 1)
		{
			_data = new double[init.Count, y];
			for (var i = 0; i < init.Count; i++)
				_data[i, 0] = init[i];
			_x = init.Count;
			_y = y;
		}

		public DoubleArray Select(Func<double, double> transform)
		{
			var result = new DoubleArray(_x, _y);
			foreach (var index in Index.GenerateIndices(_x,_y))
			{
				result[index] = transform(this[index]);
			}
			return result;
		}

		public DoubleArray Select(Func<double, Index, double> transform)
		{
			var result = new DoubleArray(_x,_y);
			foreach (var index in Index.GenerateIndices(_x,_y))
			{
				result[index] = transform(this[index], index);
			}
			return result;
		}

		public double this[int x, int y]
		{
			get
			{
				if (x < 0 || y < 0)
					return 0;
				return _data[x, y];
			}
			set { _data[x, y] = value; }
		}

		public double this[Index i]
		{
			get
			{
				if (i.X < 0 || i.Y < 0)
					return 0;
				return _data[i.X, i.Y];
			}
			set { _data[i.X, i.Y] = value; }
		}

		public double[] ToDoubleArray()
		{
			var result = new double[_x];
			for (var i = 0; i < _x; i++)
				result[i] = _data[i, 0];
			return result;
		}

		public double[] Zip(Func<double, double, double> transform)
		{
			var result = new double[_x];
			for (int i = 0; i < _x; i++)
			{
				var buffer = 0d;
				for (var j = 0; j < _y; j++)
					buffer = transform(buffer, _data[i, j]);
				result[i] = buffer;
			}
			return result;
		}

		public static double SafeSubstruct(DoubleArray array, Index i)
		{
			if (i.X == 0)
				return array[i];
			return array[i] - array[i.PrevX()];
		}
	}

	public class Index
	{
		public int X { get; set; }
		public int Y { get; set; }

		public Index() { }

		public Index(int x, int y)
		{
			X = x;
			Y = y;
		}
		public static IEnumerable<Index> GenerateIndices(int x, int y)
		{
			for (int i=0; i<x; i++)
				for (int j=0; j<y; j++)
					yield return new Index {X = i, Y = j};
		}

		public Index PrevX() => new Index(X -1, Y);
	}
}