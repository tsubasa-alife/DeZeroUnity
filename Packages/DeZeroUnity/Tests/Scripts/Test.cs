using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using DeZeroUnity;

public class Test : MonoBehaviour
{
	private void Start()
	{
		// 1行1列の行列を作成。初期値は0.5
		var x = new Variable(Matrix<float>.Build.Dense(1, 1, 0.5f));
		Debug.Log(x.Data);
		var f1 = new Square();
		var f2 = new Exp();
		var f3 = new Square();
		var a = f1.Calculate(x);
		var b = f2.Calculate(a);
		var y = f3.Calculate(b);
		y.Backward();
		Debug.Log(x.Grad);
	}
}
