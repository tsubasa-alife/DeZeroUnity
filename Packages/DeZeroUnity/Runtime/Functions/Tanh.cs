using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;

namespace DeZeroUnity
{
	public class Tanh : Function
	{
		public override List<Matrix<float>> Forward(List<Matrix<float>> xs)
		{
			var ys = new List<Matrix<float>>();
			var x = xs[0];
			ys.Add(x.PointwiseTanh());
			return ys;
		}
		
		public override List<Variable> Backward(List<Variable> gys)
		{
			var y = Outputs[0];
			var gx = gys[0] * (1 - y * y);
			var gxs = new List<Variable> { gx };
			return gxs;
		}
	}
}