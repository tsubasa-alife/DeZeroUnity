using MathNet.Numerics.LinearAlgebra;

namespace DeZeroUnity
{
	public class Square : Function
	{
		protected override Matrix<float> Forward(Matrix<float> x)
		{
			return x.PointwisePower(2);
		}
		
		protected override Matrix<float> Backward(Matrix<float> gy)
		{
			var x = Input.Data;
			var gx = 2 * x * gy;
			return gx;
		}
	}
}
