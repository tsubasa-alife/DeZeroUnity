using MathNet.Numerics.LinearAlgebra;

namespace DeZeroUnity
{
	public class Variable
	{
		public Variable(Matrix<float> data)
		{
			Data = data;
		}
	
		public Matrix<float> Data { get; set; }
	}
}

