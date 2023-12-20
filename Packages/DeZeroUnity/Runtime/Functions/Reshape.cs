using System;
using System.Collections.Generic;
using DeZeroUnity.Algebra;

namespace DeZeroUnity
{
	public class Reshape : Function
	{
		public Reshape(Tuple<int,int> shape)
		{
			Shape = shape;
		}
		
		public Tuple<int,int> Shape { get; }
		public Tuple<int,int> XShape { get; set; }

		public override List<Matrix> Forward(List<Matrix> xs)
		{
			var x = xs[0];
			XShape = new Tuple<int, int>(x.Rows, x.Columns);
			var y = x.Reshape(Shape.Item1, Shape.Item2);
			var ys = new List<Matrix> { y };
			return ys;
		}
		
		public override List<Variable> Backward(List<Variable> gys)
		{
			return Dzf.Reshape(gys[0], XShape);
		}
	}
}