using System;
using System.Collections.Generic;
using DeZeroUnity.Algebra;

namespace DeZeroUnity
{
	public class Sum : Function
	{
		public Sum(int axis = -1)
		{
			Axis = axis;
		}
		
		public Tuple<int,int> XShape { get; set; }
		public int Axis { get; set; }

		public override List<Matrix> Forward(List<Matrix> xs)
		{
			XShape = new Tuple<int, int>(xs[0].Rows, xs[0].Columns);
			var y = xs[0].Sum(Axis);
			var ys = new List<Matrix> { y };
			return ys;
		}
		
		public override List<Variable> Backward(List<Variable> gys)
		{
			var gy = gys[0];
			var gx = Dzf.BroadcastTo(gy, XShape);
			return gx;
		}
	}
}