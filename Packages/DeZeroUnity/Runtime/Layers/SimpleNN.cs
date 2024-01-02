namespace DeZeroUnity
{
	public class SimpleNN : Model
	{
		public Layer L1 { get; set; }
		public Layer L2 { get; set; }
		
		public SimpleNN(int inputSize, int hiddenSize, int outputSize)
		{
			L1 = new Linear(inputSize, hiddenSize);
			Layers.Add(L1);
			L2 = new Linear(hiddenSize, outputSize);
			Layers.Add(L2);
		}
		
		public override Variable Forward(Variable x)
		{
			var y1 = L1.Calculate(x);
			var y2 = Dzf.Tanh(y1)[0];
			var y3 = L2.Calculate(y2);
			var y4 = Dzf.Tanh(y3)[0];
			return y4;
		}
	}
}