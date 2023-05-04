using MathNet.Numerics.LinearAlgebra;

namespace DeZeroUnity
{
	public class Square : Function
	{
		protected override Matrix<float> Forward(Matrix<float> x)
		{
			return x.PointwisePower(2);
		}
	}
}
