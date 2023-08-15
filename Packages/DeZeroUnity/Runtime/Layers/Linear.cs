using MathNet.Numerics.LinearAlgebra;

namespace DeZeroUnity
{
	public class Linear : Layer
	{

		public Linear(int inSize, int outSize, bool noBias = false)
		{
			InSize = inSize;
			OutSize = outSize;
			//ランダムな値による初期化
			W = new Parameter(Matrix<float>.Build.Random(inSize, outSize));
			Params.Add(W);
			if (!noBias)
			{
				// note: 本来であれば(outsize, 1)の行列を作るべきだが
				//       BroadCast周りの実装が難しいので(1, 1)の行列を作る
				Bias = new Parameter(Matrix<float>.Build.Random(1, 1));
				Params.Add(Bias);
			}
		}
		
		public int InSize { get; set; }
		public int OutSize { get; set; }

		public Parameter W { get; set; }
		public Parameter Bias { get; set; }
		
		public override Variable Forward(Variable x)
		{
			var y = Dzf.Linear(x, W, Bias)[0];
			return y;
		}
	}
}