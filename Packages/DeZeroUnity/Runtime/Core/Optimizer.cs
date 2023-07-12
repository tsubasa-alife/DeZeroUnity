namespace DeZeroUnity
{
	public abstract class Optimizer
	{
		public Optimizer()
		{
			Target = null;
		}
		
		public Layer Target { get; set; }
		
		public void Setup(Layer target)
		{
			Target = target;
		}
		
		public void Update()
		{
			foreach (var param in Target.Params)
			{
				UpdateOne(param);
			}

			foreach (var layer in Target.Layers)
			{
				foreach (var param in layer.Params)
				{
					UpdateOne(param);
				}
			}
		}

		public abstract void UpdateOne(Parameter param);
	}
}