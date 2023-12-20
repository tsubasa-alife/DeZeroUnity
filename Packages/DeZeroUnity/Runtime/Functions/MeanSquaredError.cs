using System.Collections.Generic;
using DeZeroUnity.Algebra;

namespace DeZeroUnity
{
	public class MeanSquaredError : Function
	{
		public override List<Matrix> Forward(List<Matrix> xs)
		{
			var ys = new List<Matrix>();
			var x0 = xs[0];
			var x1 = xs[1];
			var diff = x0 - x1;
			var square = diff.Power();
			var sum = square.Sum();
			var y = new Matrix(1, 1, sum.Elements[0, 0] / square.Rows);
			ys.Add(y);
			return ys;
		}
		
		public override List<Variable> Backward(List<Variable> gys)
		{
			var x0 = Inputs[0];
			var x1 = Inputs[1];
			var gy = gys[0];
			var diff = x0 - x1;
			var gx0 = gy * diff * (2.0f / diff.Ndim);
			var gx1 = -gx0;
			var gxs = new List<Variable> { gx0, gx1 };
			return gxs;
		}
	}
}