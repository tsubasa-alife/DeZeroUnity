using System;
using System.Collections.Generic;

namespace DeZeroUnity
{
	/// <summary>
	/// Functionを簡単に使うための静的メソッドを記述したクラス
	/// </summary>
	public  static class Dzf
	{
		public static List<Variable> Square(List<Variable> xs)
		{
			return new Square().Calculate(xs);
		}
		
		public static List<Variable> Exp(List<Variable> xs)
		{
			return new Exp().Calculate(xs);
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
		
		public static List<Variable> Sum(Variable x)
		{
			return new Sum().Calculate(new List<Variable> {x});
		}
		
		public static List<Variable> BroadcastTo(Variable x, Tuple<int,int> shape)
		{
			if (Equals(x.Shape, shape))
			{
				return new List<Variable> {x};
			}
			return new BroadcastTo(shape).Calculate(new List<Variable> {x});
		}
	}
}