using System;
using MathNet.Numerics.LinearAlgebra;

namespace DeZeroUnity
{
	public class MatrixUtils
	{
		public static float SumMatrix(Matrix<float> x)
		{
			var array = x.ToRowMajorArray();
			float sum = 0;
			foreach (var element in array)
			{
				sum += element;
			}
			return sum;
		}
		
		public static Matrix<float> BroadcastToMatrix(Matrix<float> x, Tuple<int, int> shape)
		{
			if (x.RowCount == 1 && x.ColumnCount == 1)
			{
				return Matrix<float>.Build.Dense(shape.Item1, shape.Item2, x[0, 0]);
			}
			else if (x.RowCount == 1 && x.ColumnCount == shape.Item2)
			{
				var y = Matrix<float>.Build.Dense(shape.Item1, shape.Item2);
				for (int i = 0; i < shape.Item1; i++)
				{
					for (int j = 0; j < shape.Item2; j++)
					{
						y[i, j] = x[0, j];
					}
				}

				return y;
			}
			else if (x.RowCount == shape.Item1 && x.ColumnCount == 1)
			{
				var y = Matrix<float>.Build.Dense(shape.Item1, shape.Item2);
				for (int i = 0; i < shape.Item1; i++)
				{
					for (int j = 0; j < shape.Item2; j++)
					{
						y[i, j] = x[i, 0];
					}
				}

				return y;
			}
			else
			{
				throw new Exception("ブロードキャストできません");
			}
			
		}
		
	}
}