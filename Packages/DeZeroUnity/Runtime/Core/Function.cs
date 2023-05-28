using System;
using MathNet.Numerics.LinearAlgebra;

namespace DeZeroUnity
{
	public abstract class Function
	{
		public Variable Input { get; set; }
		public Variable Output { get; set; }

		public Variable Calculate(Variable input)
		{
			var x = input.Data;
			var y = Forward(x);
			var output = new Variable(y);
			Input = input;
			Output = output;
			return output;
		}

		protected abstract Matrix<float> Forward(Matrix<float> x);
		protected abstract Matrix<float> Backward(Matrix<float> gy);

	}
}

