using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;

namespace DeZeroUnity
{
	public class Sum : Function
	{
		public Sum(int? axis)
		{
			Axis = axis;
		}
		
		public Tuple<int,int> XShape { get; set; }
		public int? Axis { get; set; }

		public override List<Matrix<float>> Forward(List<Matrix<float>> xs)
		{
			XShape = new Tuple<int, int>(xs[0].RowCount, xs[0].ColumnCount);
			var y = MatrixUtils.SumMatrix(xs[0], Axis);
			var ys = new List<Matrix<float>> { y };
			return ys;
		}
		
		public override List<Variable> Backward(List<Variable> gys)
		{
			var gy = gys[0];
			var gx = Dzf.BroadcastTo(gy, XShape);
			return gx;
		}
	}
}