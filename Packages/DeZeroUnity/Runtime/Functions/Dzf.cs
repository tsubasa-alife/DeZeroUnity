namespace DeZeroUnity
{
	/// <summary>
	/// Functionを簡単に使うための静的メソッドを記述したクラス
	/// </summary>
	public class Dzf
	{
		public static Variable Square(Variable x)
		{
			return new Square().Calculate(x);
		}
		
		public static Variable Exp(Variable x)
		{
			return new Exp().Calculate(x);
		}
	}
}