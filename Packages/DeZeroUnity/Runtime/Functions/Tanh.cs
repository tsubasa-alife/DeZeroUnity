using System.Collections.Generic;
using DeZeroUnity.Algebra;

namespace DeZeroUnity
{
	public class Tanh : Function
	{
		public override List<Matrix> Forward(List<Matrix> xs)
		{
			var ys = new List<Matrix>();
			var x = xs[0];
			ys.Add(x.Tanh());
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