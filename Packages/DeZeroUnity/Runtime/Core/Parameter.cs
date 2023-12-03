using DeZeroUnity.Algebra;

namespace DeZeroUnity
{
	/// <summary>
	/// 学習パラメタと変数(Variable)を区別するためのクラス
	/// </summary>
	public class Parameter : Variable
	{
		public Parameter(Matrix data): base(data)
		{
		}
		
	}
}