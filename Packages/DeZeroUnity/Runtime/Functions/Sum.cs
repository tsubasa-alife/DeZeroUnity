using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;

namespace DeZeroUnity
{
	public class Sum : Function
	{
		public Tuple<int,int> XShape { get; set; }
		
		public override List<Matrix<float>> Forward(List<Matrix<float>> xs)
		{
			XShape = new Tuple<int, int>(xs[0].RowCount, xs[0].ColumnCount);
			var y = SumMatrix(xs[0]);
			var ys = new List<Matrix<float>> { Matrix<float>.Build.Dense(1, 1, y) };
			return ys;
		}
		
		public override List<Variable> Backward(List<Variable> gys)
		{
			throw new System.NotImplementedException();
		}
		
		private float SumMatrix(Matrix<float> x)
		{
			var array = x.ToRowMajorArray();
			float sum = 0;
			foreach (var element in array)
			{
				sum += element;
			}
			return sum;
		}
	}
}