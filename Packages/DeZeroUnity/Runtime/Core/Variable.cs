using System.Collections.Generic;
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
			if (this.Grad == null)
			{
				this.Grad = Matrix<float>.Build.Dense(Data.RowCount, Data.ColumnCount, 1.0f);
			}
			
			var functions = new Stack<Function>();
			functions.Push(Creator);
			while (functions.Count > 0)
			{
				var function = functions.Pop();
				var x = function.Input;
				var y = function.Output;
				x.Grad = function.Backward(y.Grad);
				
				if (x.Creator != null)
				{
					functions.Push(x.Creator);
				}
			}
		}
	}
}

