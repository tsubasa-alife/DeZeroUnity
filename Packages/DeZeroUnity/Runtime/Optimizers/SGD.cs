namespace DeZeroUnity
{
	public class SGD : Optimizer
	{
		public SGD(float lr)
		{
			LearningRate = lr;
		}
		
		public float LearningRate { get; set; }
		
		public override void UpdateOne(Parameter param)
		{
			param.Data -= LearningRate * param.Grad.Data;
		}
	}
}