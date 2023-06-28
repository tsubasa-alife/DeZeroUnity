using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;

namespace DeZeroUnity
{
	public class Reshape : Function
	{
		public Reshape(Tuple<int,int> shape)
		{
			Shape = shape;
		}
		
		public Tuple<int,int> Shape { get; }
		public Tuple<int,int> XShape { get; set; }

		public override List<Matrix<float>> Forward(List<Matrix<float>> xs)
		{
			var x = xs[0];
			XShape = new Tuple<int, int>(x.RowCount, x.ColumnCount);
			var y = ReshapeMatrix(x, Shape);
			var ys = new List<Matrix<float>> { y };
			return ys;
		}
		
		public override List<Variable> Backward(List<Variable> gys)
		{
			return Dzf.Reshape(gys[0], XShape);
		}
		
		private Matrix<float> ReshapeMatrix(Matrix<float> x, Tuple<int,int> shape)
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
	}
}