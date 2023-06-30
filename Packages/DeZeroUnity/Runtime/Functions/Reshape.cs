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
			var y = MatrixUtils.ReshapeMatrix(x, Shape);
			var ys = new List<Matrix<float>> { y };
			return ys;
		}
		
		public override List<Variable> Backward(List<Variable> gys)
		{
			return Dzf.Reshape(gys[0], XShape);
		}
	}
}