using System;
using System.Collections.Generic;

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
		
		/// <summary>
		/// パラメタを一つのリストにまとめる
		/// </summary>
		public List<float[,]> FlattenParams()
		{
			var result = new List<float[,]>();
			
			foreach (var param in Params)
			{
				result.Add(param.Data.Elements);
			}
			
			foreach (var layer in Layers)
			{
				foreach (var param in layer.Params)
				{
					result.Add(param.Data.Elements);
				}
			}

			return result;
		}
		
		
	}
}