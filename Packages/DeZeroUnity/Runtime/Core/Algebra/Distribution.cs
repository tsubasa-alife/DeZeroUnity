using System;

namespace DeZeroUnity.Algebra
{
	public static class Distribution
	{
		public static float NormalRandom(Random rand, float mu = 0, float sigma = 1)
		{
			// Box-Muller法による正規乱数の生成
			var alpha = (float)rand.NextDouble();
			var beta = (float)rand.NextDouble();
			var z = sigma * MathF.Sqrt(-2.0f * MathF.Log(alpha)) * MathF.Sin(2.0f * MathF.PI * beta) + mu;
			return z;
		}
		
	}
}