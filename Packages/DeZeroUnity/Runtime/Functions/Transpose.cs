using System.Collections.Generic;
using DeZeroUnity.Algebra;

namespace DeZeroUnity
{
	public class Transpose : Function
	{
		public override List<Matrix> Forward(List<Matrix> xs)
		{
			var x = xs[0];
			var y = x.Transpose();
			return new List<Matrix> { y };
		}
		
		public override List<Variable> Backward(List<Variable> gys)
		{
			var gy = gys[0];
			var gx = Dzf.Transpose(gy);
			return gx;
		}
	}
}