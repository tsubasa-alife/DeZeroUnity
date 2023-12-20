using System;
using System.Collections.Generic;
using System.Linq;
using DeZeroUnity.Algebra;

namespace DeZeroUnity
{
	public class Variable
	{
		public Variable(Matrix data)
		{
			Data = data;
			Generation = 0;
		}
	
		public Matrix Data { get; set; }
		public Variable Grad { get; set; }
		private Function Creator { get; set; }
		public int Generation { get; set; }
		
		public Tuple<int, int> Shape => new Tuple<int, int>(Data.Rows, Data.Columns);
		public int Ndim => Data.Rows;
		public int Size => Data.Rows * Data.Columns;


		public void SetCreator(Function func)
		{
			Creator = func;
			Generation = func.Generation + 1;
		}
		
		public void ClearGrads()
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

		public void Backward(bool retainGrad = false, bool createGraph = false)
		{
			if (this.Grad == null)
			{
				this.Grad = new Variable(new Matrix(Data.Rows, Data.Columns, 1.0f));
			}
			
			var functions = new Stack<Function>();
			var seenSet = new HashSet<Function>();
			AddFunc(ref functions, seenSet, Creator);
			
			while (functions.Count > 0)
			{
				var function = functions.Pop();
				var gys = new List<Variable>();
				foreach (var output in function.Outputs)
				{
					gys.Add(output.Grad);
				}

				using (ConfigUtils.UsingConfig("enableBackprop", createGraph))
				{
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
				if (!retainGrad)
				{
					foreach (var y in function.Outputs)
					{
						y.ClearGrads();
					}
				}
			}
		}

		// 演算子オーバーロード
		public static Variable operator +(Variable a, Variable b)
		{
			return Dzf.Add(a, b)[0];
		}
		
		public static Variable operator +(Variable a, float b)
		{
			var bVar = new Variable(new Matrix(a.Data.Rows, a.Data.Columns, b));
			return Dzf.Add(a, bVar)[0];
		}
		
		public static Variable operator +(float a, Variable b)
		{
			var aVar = new Variable(new Matrix(b.Data.Rows, b.Data.Columns, a));
			return Dzf.Add(aVar, b)[0];
		}
		
		public static Variable operator -(Variable a)
		{
			return Dzf.Neg(a)[0];
		}
		
		public static Variable operator -(Variable a, Variable b)
		{
			return Dzf.Sub(a, b)[0];
		}
		
		public static Variable operator -(Variable a, float b)
		{
			var bVar = new Variable(new Matrix(a.Data.Rows, a.Data.Columns, b));
			return Dzf.Sub(a, bVar)[0];
		}
		
		public static Variable operator -(float a, Variable b)
		{
			var aVar = new Variable(new Matrix(b.Data.Rows, b.Data.Columns, a));
			return Dzf.Sub(aVar, b)[0];
		}
		
		public static Variable operator *(Variable a, Variable b)
		{
			return Dzf.Mul(a, b)[0];
		}
		
		public static Variable operator *(Variable a, float b)
		{
			var bVar = new Variable(new Matrix(a.Data.Rows, a.Data.Columns, b));
			return Dzf.Mul(a, bVar)[0];
		}
		
		public static Variable operator *(float a, Variable b)
		{
			var aVar = new Variable(new Matrix(b.Data.Rows, b.Data.Columns, a));
			return Dzf.Mul(aVar, b)[0];
		}
		
		public static Variable operator /(Variable a, Variable b)
		{
			return Dzf.Div(a, b)[0];
		}
		
		public static Variable operator /(Variable a, float b)
		{
			var bVar = new Variable(new Matrix(a.Data.Rows, a.Data.Columns, b));
			return Dzf.Div(a, bVar)[0];
		}
		
		public static Variable operator /(float a, Variable b)
		{
			var aVar = new Variable(new Matrix(b.Data.Rows, b.Data.Columns, a));
			return Dzf.Div(aVar, b)[0];
		}
		
		public static Variable operator ^(Variable a, float b)
		{
			return Dzf.Pow(a, b)[0];
		}
	}
}

