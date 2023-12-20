using System;
using System.Collections.Generic;
using System.Linq;
using DeZeroUnity.Algebra;

namespace DeZeroUnity
{
	public abstract class Function
	{
		public List<Variable> Inputs { get; set; }
		public List<Variable> Outputs { get; set; }
		public int Generation { get; set; }

		public List<Variable> Calculate(List<Variable> inputs)
		{
			var xs = new List<Matrix>();
			foreach (var input in inputs)
			{
				xs.Add(input.Data);
			}
			var ys = Forward(xs);
			var outputs = new List<Variable>();
			foreach (var y in ys)
			{
				outputs.Add(new Variable(y));
			}

			if (Config.enableBackprop)
			{
				Generation = inputs.Max(x => x.Generation);
				foreach (var output in outputs)
				{
					output.SetCreator(this);
				}
				Inputs = inputs;
				Outputs = outputs;
			}
			return outputs;
		}

		public abstract List<Matrix> Forward(List<Matrix> xs);
		public abstract List<Variable> Backward(List<Variable> gys);

	}
}

