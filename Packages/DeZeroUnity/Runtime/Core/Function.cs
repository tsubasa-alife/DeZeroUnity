using System;
using MathNet.Numerics.LinearAlgebra;

namespace DeZeroUnity
{
	public abstract class Function
	{
		public Variable Calculate(Variable input)
		{
			var x = input.Data;
			var y = Forward(x);
			var output = new Variable(y);
			return output;
		}

		protected abstract Matrix<float> Forward(Matrix<float> x);

	}
}

