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
	}
}