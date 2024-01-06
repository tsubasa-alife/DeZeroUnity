using System;

namespace DeZeroUnity.Algebra
{
	/// <summary>
	/// 確率分布に関連する乱数生成用クラス
	/// </summary>
	public static class Distribution
	{
		/// <summary>
		/// Box-Muller法による正規乱数の生成
		/// </summary>
		/// <param name="rand"></param>
		/// <param name="mu">平均</param>
		/// <param name="sigma">分散</param>
		/// <returns></returns>
		public static float NormalRandom(Random rand, float mu = 0, float sigma = 1)
		{
			var alpha = (float)rand.NextDouble();
			var beta = (float)rand.NextDouble();
			var z = sigma * MathF.Sqrt(-2.0f * MathF.Log(alpha)) * MathF.Sin(2.0f * MathF.PI * beta) + mu;
			return z;
		}
		
	}
}