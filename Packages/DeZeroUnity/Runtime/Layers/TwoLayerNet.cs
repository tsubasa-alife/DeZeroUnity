using MathNet.Numerics.LinearAlgebra;

namespace DeZeroUnity
{
	public class TwoLayerNet : Layer
	{
		public Layer L1 { get; set; }
		public Layer L2 { get; set; }
		
		public TwoLayerNet(int inputSize, int hiddenSize, int outputSize)
		{
			L1 = new Linear(inputSize, hiddenSize);
			Layers.Add(L1);
			L2 = new Linear(hiddenSize, outputSize);
			Layers.Add(L2);
		}
		
		public override Variable Forward(Variable x)
		{
			var y1 = L1.Calculate(x);
			var y2 = Dzf.Sigmoid(y1)[0];
			var y3 = L2.Calculate(y2);
			return y3;
		}
	}
	
}