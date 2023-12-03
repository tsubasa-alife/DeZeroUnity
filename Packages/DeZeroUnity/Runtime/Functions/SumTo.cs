using System;
using System.Collections.Generic;
using DeZeroUnity.Algebra;

namespace DeZeroUnity
{
	public class SumTo : Function
	{
		
		public SumTo(Tuple<int, int> shape)
		{
			Shape = shape;
		}

		public Tuple<int, int> Shape { get; set; }
		public Tuple<int, int> XShape { get; set; }
		
		public override List<Matrix> Forward(List<Matrix> xs)
		{
			XShape = new Tuple<int, int>(xs[0].Rows, xs[0].Columns);
			var x = xs[0];
			var y = x.SumTo(Shape.Item1, Shape.Item2);
			return new List<Matrix> { y };
		}
		
		public override List<Variable> Backward(List<Variable> gys)
		{
			var gy = gys[0];
			var gx = Dzf.BroadcastTo(gy, XShape);
			return gx;
		}
	} 
}