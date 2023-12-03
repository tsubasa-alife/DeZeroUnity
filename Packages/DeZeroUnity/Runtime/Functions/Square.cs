using System.Collections.Generic;
using DeZeroUnity.Algebra;

namespace DeZeroUnity
{
	public class Square : Function
	{
		public override List<Matrix> Forward(List<Matrix> xs)
		{
			var ys = new List<Matrix>();
			var x = xs[0];
			ys.Add(x.Power());
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
