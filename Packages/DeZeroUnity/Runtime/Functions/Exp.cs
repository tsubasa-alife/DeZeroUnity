using System.Collections.Generic;
using DeZeroUnity.Algebra;

namespace DeZeroUnity
{
	public class Exp : Function
	{
		public override List<Matrix> Forward(List<Matrix> xs)
		{
			var ys = new List<Matrix>();
			var x = xs[0];
			ys.Add(x.Exp());
			return ys;
		}
		
		public override List<Variable> Backward(List<Variable> gys)
		{
			var y = Outputs[0];
			var gy = gys[0];
			var gx = gy * y;
			var gxs = new List<Variable> { gx };
			return gxs;
		}
		
	}
	
	
	
}