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
				Bias = new Parameter(Matrix<float>.Build.Random(outSize, 1));
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