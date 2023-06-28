using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;

namespace DeZeroUnity
{
	public class Transpose : Function
	{
		public override List<Matrix<float>> Forward(List<Matrix<float>> xs)
		{
			var x = xs[0];
			var y = x.Transpose();
			return new List<Matrix<float>> { y };
		}
		
		public override List<Variable> Backward(List<Variable> gys)
		{
			var gy = gys[0];
			var gx = Dzf.Transpose(gy);
			return gx;
		}
	}
}