using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;

namespace DeZeroUnity
{
	public abstract class Function
	{
		public List<Variable> Inputs { get; set; }
		public List<Variable> Outputs { get; set; }
		public int Generation { get; set; }

		public List<Variable> Calculate(List<Variable> inputs)
		{
			var xs = new List<Matrix<float>>();
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

		public abstract List<Matrix<float>> Forward(List<Matrix<float>> xs);
		public abstract List<Matrix<float>> Backward(List<Matrix<float>> gys);

	}
}

