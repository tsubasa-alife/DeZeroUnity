using MathNet.Numerics.LinearAlgebra;

namespace DeZeroUnity
{
	public class Exp : Function
	{
		public override Matrix<float> Forward(Matrix<float> x)
		{
			return x.PointwiseExp();
		}
		
		public override Matrix<float> Backward(Matrix<float> gy)
		{
			var x = Input.Data;
			var gx = gy.PointwiseMultiply(x.PointwiseExp());
			return gx;
		}

	}
}