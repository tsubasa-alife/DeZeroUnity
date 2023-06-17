using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;

namespace DeZeroUnity
{
	public class Sub : Function
	{
		public override List<Matrix<float>> Forward(List<Matrix<float>> xs)
		{
			var ys = new List<Matrix<float>>();
			var x0 = xs[0];
			var x1 = xs[1];
			ys.Add(x0 - x1);
			return ys;
		}
		
		public override List<Matrix<float>> Backward(List<Matrix<float>> gys)
		{
			var gy = gys[0];
			var gx0 = gy;
			var gx1 = -gy;
			var gxs = new List<Matrix<float>> {gx0, gx1};
			return gxs;
		}
	}
}