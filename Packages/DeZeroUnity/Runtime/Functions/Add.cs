using System;
using System.Collections.Generic;
using DeZeroUnity.Algebra;

namespace DeZeroUnity
{
	public class Add : Function
	{
		public Tuple<int,int> X0Shape { get; set; }
		public Tuple<int,int> X1Shape { get; set; }
		
		public override List<Matrix> Forward(List<Matrix> xs)
		{
			var ys = new List<Matrix>();
			var x0 = xs[0];
			var x1 = xs[1];
			X0Shape = new Tuple<int, int>(x0.Rows, x0.Columns);
			X1Shape = new Tuple<int, int>(x1.Rows, x1.Columns);
			//X0ShapeとX1Shapeが比較して要素数が大きい方の形状に合わせるようにブロードキャストする
			if (X0Shape.Item1 * X0Shape.Item2 > X1Shape.Item2 * X1Shape.Item2)
			{
				x1 = x1.Broadcast(X0Shape.Item1, X0Shape.Item2);
			}
			else if (X0Shape.Item1 * X0Shape.Item2 < X1Shape.Item2 * X1Shape.Item2)
			{
				x0 = x0.Broadcast(X1Shape.Item1, X1Shape.Item2);
			}
			
			ys.Add(x0 + x1);
			return ys;
		}
		
		public override List<Variable> Backward(List<Variable> gys)
		{
			var gy = gys[0];
			var gx0 = gy;
			var gx1 = gy;
			
			if(!Equals(X0Shape, X1Shape))
			{
				gx0 = Dzf.SumTo(gy, X0Shape)[0];
				gx1 = Dzf.SumTo(gy, X1Shape)[0];
			}
			var gxs = new List<Variable> {gx0, gx1};
			
			return gxs;
		}
	}
}