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
	}
}