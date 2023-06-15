using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;

namespace DeZeroUnity
{
	public class Mul : Function
	{
		public override List<Matrix<float>> Forward(List<Matrix<float>> xs)
		{
			var ys = new List<Matrix<float>>();
			var x0 = xs[0];
			var x1 = xs[1];
			ys.Add(x0.PointwiseMultiply(x1));
			return ys;
		}

		public override List<Matrix<float>> Backward(List<Matrix<float>> gys)
		{
			var x0 = Inputs[0].Data;
			var x1 = Inputs[1].Data;
			var gy = gys[0];
			var gx0 = gy.PointwiseMultiply(x1);
			var gx1 = gy.PointwiseMultiply(x0);
			var gxs = new List<Matrix<float>> {gx0, gx1};
			return gxs;
		}
	}
}