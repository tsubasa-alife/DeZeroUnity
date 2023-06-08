using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;

namespace DeZeroUnity
{
	public class Square : Function
	{
		public override List<Matrix<float>> Forward(List<Matrix<float>> xs)
		{
			var ys = new List<Matrix<float>>();
			var x = xs[0];
			ys.Add(x.PointwisePower(2));
			return ys;
		}
		
		public override List<Matrix<float>> Backward(List<Matrix<float>> gys)
		{
			var x = Inputs[0].Data;
			var gx = 2 * x * gys[0];
			var gxs = new List<Matrix<float>> { gx };
			return gxs;
		}
	}
}
