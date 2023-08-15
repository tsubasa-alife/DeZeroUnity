using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;

namespace DeZeroUnity
{
	public class MeanSquaredError : Function
	{
		public override List<Matrix<float>> Forward(List<Matrix<float>> xs)
		{
			var ys = new List<Matrix<float>>();
			var x0 = xs[0];
			var x1 = xs[1];
			var diff = x0 - x1;
			var square = diff.PointwisePower(2);
			var sum = Matrix<float>.Build.Dense(1, 1, 0);
			for (int i = 0; i < square.RowCount; i++)
			{
				for (int j = 0; j < square.ColumnCount; j++)
				{
					sum[0, 0] += square[i, j];
				}
			}
			var y = Matrix<float>.Build.Dense(1, 1, sum[0, 0] / square.RowCount);
			ys.Add(y);
			return ys;
		}
		
		public override List<Variable> Backward(List<Variable> gys)
		{
			var x0 = Inputs[0];
			var x1 = Inputs[1];
			var gy = gys[0];
			var diff = x0 - x1;
			var gx0 = gy * diff * (2.0f / diff.Ndim);
			var gx1 = -gx0;
			var gxs = new List<Variable> { gx0, gx1 };
			return gxs;
		}
	}
}