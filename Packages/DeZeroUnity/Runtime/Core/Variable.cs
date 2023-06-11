using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;
using UnityEngine;

namespace DeZeroUnity
{
	public class Variable
	{
		public Variable(Matrix<float> data)
		{
			Data = data;
			Generation = 0;
		}
	
		public Matrix<float> Data { get; set; }
		public Matrix<float> Grad { get; set; }
		private Function Creator { get; set; }
		public int Generation { get; set; }

		public void SetCreator(Function func)
		{
			Creator = func;
			Generation = func.Generation + 1;
		}
		
		public void ClearGrad()
		{
			this.Grad = null;
		}

		private void AddFunc(ref Stack<Function> functions, HashSet<Function> seenSet, Function function)
		{
			if (!seenSet.Contains(function))
			{
				functions.Push(function);
				seenSet.Add(function);
				functions = new Stack<Function>(functions.OrderBy(x => x.Generation));
			}
		}

		public void Backward()
		{
			if (this.Grad == null)
			{
				this.Grad = Matrix<float>.Build.Dense(Data.RowCount, Data.ColumnCount, 1.0f);
			}
			
			var functions = new Stack<Function>();
			var seenSet = new HashSet<Function>();
			AddFunc(ref functions, seenSet, Creator);
			
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
					if (x.Grad == null)
					{
						x.Grad = gx;
					}
					else
					{
						x.Grad = x.Grad + gx;
					}
					
					if (x.Creator != null)
					{
						AddFunc(ref functions, seenSet, x.Creator);
					}
				}
			}
		}
	}
}

