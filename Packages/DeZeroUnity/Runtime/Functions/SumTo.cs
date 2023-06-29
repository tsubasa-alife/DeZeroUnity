using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;

namespace DeZeroUnity
{
	public class SumTo : Function
	{
		
		public SumTo(Tuple<int, int> shape)
		{
			Shape = shape;
		}

		public Tuple<int, int> Shape { get; set; }
		public Tuple<int, int> XShape { get; set; }
		
		public override List<Matrix<float>> Forward(List<Matrix<float>> xs)
		{
			XShape = new Tuple<int, int>(xs[0].RowCount, xs[0].ColumnCount);
			var x = xs[0];
			var y = MatrixUtils.SumToMatrix(x, Shape);
			return new List<Matrix<float>> { y };
		}
		
		public override List<Variable> Backward(List<Variable> gys)
		{
			var gy = gys[0];
			var gx = Dzf.BroadcastTo(gy, XShape);
			return gx;
		}
	} 
}