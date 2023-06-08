using System.Collections.Generic;
using System.Linq;
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
				var gys = new List<Matrix<float>>();
				foreach (var output in function.Outputs)
				{
					gys.Add(output.Grad);
				}
				var gxs = function.Backward(gys);
				// function.Inputsとgxsをzip
				foreach (var (x, gx) in function.Inputs.Zip(gxs, (x, gx) => (x, gx)))
				{
					x.Grad = gx;
					if (x.Creator != null)
					{
						functions.Push(x.Creator);
					}
				}
			}
		}
	}
}

