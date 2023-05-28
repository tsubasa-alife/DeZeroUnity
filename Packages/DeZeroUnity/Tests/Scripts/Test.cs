using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using DeZeroUnity;

public class Test : MonoBehaviour
{
	private void Start()
	{
		var x = new Variable(Matrix<float>.Build.Random(2, 3));
		Debug.Log(x.Data);
		var f = new Exp();
		var y = f.Calculate(x);
		Debug.Log(y.Data);
	}
}
