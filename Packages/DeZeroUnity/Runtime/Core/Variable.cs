using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;

namespace DeZeroUnity
{
	public class Variable
	{
		public Variable(Matrix<float> data)
		{
			Data = data;
		}
	
		public Matrix<float> Data { get; set; }
		public Matrix<float> Grad { get; set; }
		private Function Creator { get; set; }

		public void SetCreator(Function func)
		{
			Creator = func;
		}

		public void Backward()
		{
			var function = Creator;
			if (function != null)
			{
				var x = function.Input;
				x.Grad = function.Backward(this.Grad);
				x.Backward();
			}
		}
	}
}

