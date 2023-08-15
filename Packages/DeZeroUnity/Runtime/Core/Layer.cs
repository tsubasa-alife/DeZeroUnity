using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;

namespace DeZeroUnity
{
	public abstract class Layer
	{
		public Layer()
		{
			Params = new HashSet<Parameter>();
			Layers = new HashSet<Layer>();
		}

		public HashSet<Parameter> Params { get; set; }
		public HashSet<Layer> Layers { get; set; }

		public Variable Calculate(Variable x)
		{
			var y = Forward(x);
			return y;
		}

		public abstract Variable Forward(Variable x);
		
		public void ClearGrads()
		{
			foreach (var param in Params)
			{
				param.ClearGrads();
			}
			
			foreach (var layer in Layers)
			{
				layer.ClearGrads();
			}
		}
		
		
	}
}