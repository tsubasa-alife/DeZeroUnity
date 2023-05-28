using MathNet.Numerics.LinearAlgebra;

namespace DeZeroUnity
{
	public class Exp : Function
	{
		protected override Matrix<float> Forward(Matrix<float> x)
		{
			return x.PointwiseExp();
		}
		
		protected override Matrix<float> Backward(Matrix<float> gy)
		{
			var x = Input.Data;
			var gx = gy.PointwiseMultiply(x.PointwiseExp());
			return gx;
		}

	}
}