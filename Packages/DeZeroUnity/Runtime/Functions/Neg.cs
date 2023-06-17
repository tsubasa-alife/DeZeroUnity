using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;

namespace DeZeroUnity
{
	public class Neg : Function
	{
		public override List<Matrix<float>> Forward(List<Matrix<float>> xs)
		{
			var ys = new List<Matrix<float>>();
			var x = xs[0];
			ys.Add(-x);
			return ys;
		}
		
		public override List<Matrix<float>> Backward(List<Matrix<float>> gys)
		{
			var gy = gys[0];
			var gx = -gy;
			var gxs = new List<Matrix<float>> { gx };
			return gxs;
		}
	}
}