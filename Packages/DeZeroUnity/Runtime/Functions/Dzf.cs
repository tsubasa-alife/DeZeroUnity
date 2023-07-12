using System;
using System.Collections.Generic;

namespace DeZeroUnity
{
	/// <summary>
	/// Functionを簡単に使うための静的メソッドを記述したクラス
	/// </summary>
	public  static class Dzf
	{
		public static List<Variable> Square(Variable x)
		{
			return new Square().Calculate(new List<Variable> {x});
		}

		public static List<Variable> Exp(Variable x)
		{
			return new Exp().Calculate(new List<Variable> {x});
		}
		
		public static List<Variable> Add(Variable x0, Variable x1)
		{
			return new Add().Calculate(new List<Variable> {x0, x1});
		}
		
		public static List<Variable> Mul(Variable x0, Variable x1)
		{
			return new Mul().Calculate(new List<Variable> {x0, x1});
		}
		
		public static List<Variable> Neg(Variable x)
		{
			return new Neg().Calculate(new List<Variable> {x});
		}
		
		public static List<Variable> Sub(Variable x0, Variable x1)
		{
			return new Sub().Calculate(new List<Variable> {x0, x1});
		}
		
		public static List<Variable> Div(Variable x0, Variable x1)
		{
			return new Div().Calculate(new List<Variable> {x0, x1});
		}
		
		public static List<Variable> Pow(Variable x, float c)
		{
			return new Pow(c).Calculate(new List<Variable> {x});
		}
		
		public static List<Variable> Sin(Variable x)
		{
			return new Sin().Calculate(new List<Variable> {x});
		}
		
		public static List<Variable> Cos(Variable x)
		{
			return new Cos().Calculate(new List<Variable> {x});
		}
		
		public static List<Variable> Tanh(Variable x)
		{
			return new Tanh().Calculate(new List<Variable> {x});
		}

		public static List<Variable> Sigmoid(Variable x)
		{
			var y = 1.0f / (1.0f + Dzf.Exp(-x)[0]);
			return new List<Variable> {y};
		}

		public static List<Variable> Reshape(Variable x, Tuple<int,int> shape)
		{
			if (Equals(x.Shape, shape))
			{
				return new List<Variable> {x};
			}
			return new Reshape(shape).Calculate(new List<Variable> {x});
		}
		
		public static List<Variable> Transpose(Variable x)
		{
			return new Transpose().Calculate(new List<Variable> {x});
		}
		
		public static List<Variable> Sum(Variable x, int? axis = null)
		{
			return new Sum(axis).Calculate(new List<Variable> {x});
		}
		
		public static List<Variable> MatMul(Variable x, Variable w)
		{
			return new MatMul().Calculate(new List<Variable> {x, w});
		}
		
		public static List<Variable> Linear(Variable x, Variable w, Variable b)
		{
			var t = MatMul(x, w);
			if (b == null)
			{
				return t;
			}
			var y = t[0] + b;
			t[0].Data = null;
			return new List<Variable> {y};
		}
		
		public static List<Variable> BroadcastTo(Variable x, Tuple<int,int> shape)
		{
			if (Equals(x.Shape, shape))
			{
				return new List<Variable> {x};
			}
			return new BroadcastTo(shape).Calculate(new List<Variable> {x});
		}
		
		public static List<Variable> SumTo(Variable x, Tuple<int,int> shape)
		{
			if (Equals(x.Shape, shape))
			{
				return new List<Variable> {x};
			}
			return new SumTo(shape).Calculate(new List<Variable> {x});
		}
	}
}