using System.Collections.Generic;
using DeZeroUnity.Algebra;

namespace DeZeroUnity
{
	public class MatMul :Function
	{
		public override List<Matrix> Forward(List<Matrix> xs)
		{
			var x = xs[0];
			var w = xs[1];
			var y = x * w;
			var ys = new List<Matrix> {y};
			return ys;
		}

		public override List<Variable> Backward(List<Variable> gys)
		{
			var x = Inputs[0];
			var w = Inputs[1];
			var gy = gys[0];
			var gx = Dzf.MatMul(gy, Dzf.Transpose(w)[0]);
			var gw = Dzf.MatMul(Dzf.Transpose(x)[0], gy);
			var gxs = new List<Variable> {gx[0], gw[0]};
			return gxs;
		}
	}
}