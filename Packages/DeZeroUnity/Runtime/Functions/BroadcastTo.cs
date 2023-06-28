using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;

namespace DeZeroUnity
{
	public class BroadcastTo : Function
	{
		public BroadcastTo(Tuple<int,int> shape)
		{
			Shape = shape;
		}
		
		public Tuple<int,int> Shape { get; set; }
		public Tuple<int,int> XShape { get; set; }

		public override List<Matrix<float>> Forward(List<Matrix<float>> xs)
		{
			XShape = new Tuple<int, int>(xs[0].RowCount, xs[0].ColumnCount);
			var x = xs[0];
			var y = BroadcastToMatrix(x, Shape);
			return new List<Matrix<float>> { y };
		}
		
		public override List<Variable> Backward(List<Variable> gys)
		{
			throw new System.NotImplementedException();
		}
		
		private Matrix<float> BroadcastToMatrix(Matrix<float> x, Tuple<int,int> shape)
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