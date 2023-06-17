using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;

namespace DeZeroUnity
{
	public class Pow : Function
	{
		public Pow(float c)
		{
			C = c;
		}
		
		// 累乗の指数
		public float C { get; }
		
		public override List<Matrix<float>> Forward(List<Matrix<float>> xs)
		{
			var ys = new List<Matrix<float>>();
			var x = xs[0];
			ys.Add(x.PointwisePower(C));
			return ys;
		}
		
		public override List<Matrix<float>> Backward(List<Matrix<float>> gys)
		{
			var x = Inputs[0].Data;
			var gx = C * x.PointwisePower(C - 1) * gys[0];
			var gxs = new List<Matrix<float>> { gx };
			return gxs;
		}
	}
}