using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;

namespace DeZeroUnity
{
	public class Add : Function
	{
		public Tuple<int,int> X0Shape { get; set; }
		public Tuple<int,int> X1Shape { get; set; }
		
		public override List<Matrix<float>> Forward(List<Matrix<float>> xs)
		{
			var ys = new List<Matrix<float>>();
			var x0 = xs[0];
			var x1 = xs[1];
			X0Shape = new Tuple<int, int>(x0.RowCount, x0.ColumnCount);
			X1Shape = new Tuple<int, int>(x1.RowCount, x1.ColumnCount);
			//X0ShapeとX1Shapeが比較して要素数が大きい方の形状に合わせるようにブロードキャストする
			if (X0Shape.Item1 * X0Shape.Item2 > X1Shape.Item2 * X1Shape.Item2)
			{
				x1 = MatrixUtils.BroadcastToMatrix(x1, X0Shape);
			}
			else if (X0Shape.Item1 * X0Shape.Item2 < X1Shape.Item2 * X1Shape.Item2)
			{
				x0 = MatrixUtils.BroadcastToMatrix(x0, X1Shape);
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