using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;

namespace DeZeroUnity
{
	public class Exp : Function
	{
		public override List<Matrix<float>> Forward(List<Matrix<float>> xs)
		{
			var ys = new List<Matrix<float>>();
			var x = xs[0];
			ys.Add(x.PointwiseExp());
			return ys;
		}
		
		public override List<Matrix<float>> Backward(List<Matrix<float>> gys)
		{
			var x = Inputs[0].Data;
			var gy = gys[0];
			var gx = gy.PointwiseMultiply(x.PointwiseExp());
			var gxs = new List<Matrix<float>> { gx };
			return gxs;
		}
		
	}
	
	
	
}