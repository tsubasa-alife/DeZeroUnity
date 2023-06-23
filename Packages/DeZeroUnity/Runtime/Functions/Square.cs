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
		
		public override List<Variable> Backward(List<Variable> gys)
		{
			var x = Inputs[0];
			var gx = 2 * x * gys[0];
			var gxs = new List<Variable> { gx };
			return gxs;
		}
	}
}
