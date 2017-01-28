using System;

namespace LearnTask
{
	public static class LearnTaskSolver
	{
		private static double[,] Solve(double[,] matrix)
		{
			var result = new double[matrix.GetLength(0), matrix.GetLength(1)];
			for (int i = 0; i < matrix.GetLength(0); i++)
				for (int j = 0; j < matrix.GetLength(1); j++)
					result[i, j] = SumAround(matrix, i, j);
			return result;
		}

		private static double SumAround(double[,] matrix, int i, int j)
		{
			var sum = 0d;
			for (int a = -1; a < 2; a++)
				for (int b = -1; b < 2; b++)
				{
					if (a == 0 && b == 0)
						continue;
					if (IsGoogIndex(matrix, i + a, j + b))
						sum += matrix[i + a, j + b];
				}
			return sum;
		}

		private static bool IsGoogIndex(double[,] matrix, int i, int j)
		{
			return !(i < 0 || j < 0 || i >= matrix.GetLength(0) || j >= matrix.GetLength(1));
		}

		public static void Run(string[] args)
		{
			var testData = new[,] 
			{
				{1.0, 3, 5},
				{2.0, 4, 6},
				{12.0, 12, 4}
			};
			var testResult = Solve(testData);
			Console.WriteLine("Original matrix");
			PrintArray(testData);
			Console.WriteLine("Transform matrix");
			PrintArray(testResult);
		}

		private static void PrintArray(double[,] matrix)
		{
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				for (int j = 0; j < matrix.GetLength(1); j++)
				{
					Console.Write(matrix[i, j].ToString().PadLeft(8));
				}
				Console.WriteLine();
			}
		}
	}
}
