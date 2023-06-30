using System;
using MathNet.Numerics.LinearAlgebra;
using UnityEngine;

namespace DeZeroUnity
{
	public class MatrixUtils
	{
		public static Matrix<float> SumMatrix(Matrix<float> x, int? axis = null)
		{
			if (axis == null)
			{
				// 全ての要素を足し合わせて1x1の行列を返す
				var sum = 0f;
				for (int i = 0; i < x.RowCount; i++)
				{
					for (int j = 0; j < x.ColumnCount; j++)
					{
						sum += x[i, j];
					}
				}
				
				return Matrix<float>.Build.Dense(1, 1, sum);
			}
			else if (axis == 0)
			{
				return Matrix<float>.Build.DenseOfRowVectors(x.ColumnSums());
			}
			else if (axis == 1)
			{
				return Matrix<float>.Build.DenseOfColumnVectors(x.RowSums());
			}
			else
			{
				throw new Exception("axisの値が不正です");
			}


		}
		
		public static Matrix<float> ReshapeMatrix(Matrix<float> x, Tuple<int,int> shape)
		{
			var rows = shape.Item1;
			var columns = shape.Item2;
			
			if (x.RowCount * x.ColumnCount != rows * columns)
			{
				throw new ArgumentException("The total number of elements does not match the new shape.");
			}
			var array = x.ToRowMajorArray();
			var reshapedArray = new float[rows, columns];
			int index = 0;
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < columns; j++)
				{
					reshapedArray[i, j] = array[index++];
				}
			}
			return Matrix<float>.Build.DenseOfArray(reshapedArray);
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
		
		public static Matrix<float> SumToMatrix(Matrix<float> x, Tuple<int, int> shape)
		{
			// xの要素についてshapeの形状になるように足し合わせる
			if (x.RowCount == shape.Item1 && x.ColumnCount == shape.Item2)
			{
				return x;
			}
			else if (shape.Item1 == 1 && x.ColumnCount == shape.Item2)
			{
				var y = Matrix<float>.Build.Dense(shape.Item1, shape.Item2);
				for (int j = 0; j < shape.Item2; j++)
				{
					for (int i = 0; i < x.RowCount; i++)
					{
						y[0, j] += x[i, j];
					}
				}
				return y;
			}
			else if (x.RowCount == shape.Item1 && shape.Item2 == 1)
			{
				var y = Matrix<float>.Build.Dense(shape.Item1, shape.Item2);
				for (int i = 0; i < shape.Item1; i++)
				{
					for (int j = 0; j < x.ColumnCount; j++)
					{
						y[i, 0] += x[i, j];
					}
				}

				return y;
			}
			else
			{
				throw new Exception("shapeの形状になるように足し合わせることができません");
			}
		
		}

	}
}