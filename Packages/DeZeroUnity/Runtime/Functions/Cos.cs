using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;

namespace DeZeroUnity
{
	public class Cos : Function
	{
		public override List<Matrix<float>> Forward(List<Matrix<float>> xs)
		{
			var ys = new List<Matrix<float>>();
			var x = xs[0];
			ys.Add(x.PointwiseCos());
			return ys;
		}
		
		public override List<Variable> Backward(List<Variable> gys)
		{
			var x = Inputs[0];
			var gy = gys[0];
			var gx = gy * -1 * Dzf.Sin(x)[0];
			var gxs = new List<Variable> { gx };
			return gxs;
		}
	}
}