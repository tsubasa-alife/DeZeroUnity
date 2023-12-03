using System;
using DeZeroUnity.Algebra;

namespace DeZeroUnity
{
	public class Linear : Layer
	{

		public Linear(int inSize, int outSize, bool noBias = false)
		{
			InSize = inSize;
			OutSize = outSize;
			//ランダムな値による初期化
			var rand = new Random();
			W = new Parameter(new Matrix(inSize, outSize, rand));
			Params.Add(W);
			if (!noBias)
			{
				// note: 本来であれば(outSize, 1)の行列を作るべきだが
				//       BroadCast周りの実装が難しいので(1, 1)の行列を作る
				Bias = new Parameter(new Matrix(1, 1, rand));
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