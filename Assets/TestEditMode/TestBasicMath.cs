using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using DeZeroUnity;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

public class TestBasicMath
{
	[Test]
	public void TestAddForward()
	{
		var x0 = new Variable(Matrix<float>.Build.Dense(1, 1, 2.0f));
		var x1 = new Variable(Matrix<float>.Build.Dense(1, 1, 3.0f));
		var ys = Dzf.Add(x0, x1);
		var expected = Matrix<float>.Build.Dense(1, 1, 5.0f);
		Assert.AreEqual(expected, ys[0].Data);
	}
    
	[Test]
	public void TestAddBackward()
	{
		var x0 = new Variable(Matrix<float>.Build.Dense(1, 1, 2.0f));
		var x1 = new Variable(Matrix<float>.Build.Dense(1, 1, 3.0f));
		var ys = Dzf.Add(x0, x1);
		ys[0].Backward();
		var expected = Matrix<float>.Build.Dense(1, 1, 1.0f);
		Assert.AreEqual(expected, x0.Grad);
		Assert.AreEqual(expected, x1.Grad);
	}
    
	[Test]
	public void TestAddSameBackward()
	{
		var x0 = new Variable(Matrix<float>.Build.Dense(1, 1, 2.0f));
		var ys = Dzf.Add(x0, x0);
		ys[0].Backward();
		var expected = Matrix<float>.Build.Dense(1, 1, 2.0f);
		Assert.AreEqual(expected, x0.Grad);
	}
	
	[Test]
	public void TestMulForward()
	{
		var x0 = new Variable(Matrix<float>.Build.Dense(1, 1, 2.0f));
		var x1 = new Variable(Matrix<float>.Build.Dense(1, 1, 3.0f));
		var ys = Dzf.Mul(x0, x1);
		var expected = Matrix<float>.Build.Dense(1, 1, 6.0f);
		Assert.AreEqual(expected, ys[0].Data);
	}
	
	[Test]
	public void TestMulBackward()
	{
		var x0 = new Variable(Matrix<float>.Build.Dense(1, 1, 3.0f));
		var x1 = new Variable(Matrix<float>.Build.Dense(1, 1, 2.0f));
		var ys = Dzf.Mul(x0, x1);
		ys[0].Backward();
		var expected0 = Matrix<float>.Build.Dense(1, 1, 2.0f);
		var expected1 = Matrix<float>.Build.Dense(1, 1, 3.0f);
		Assert.AreEqual(expected0, x0.Grad);
		Assert.AreEqual(expected1, x1.Grad);
	}
	
	[Test]
	public void TestOverLoad()
	{
		var a = new Variable(Matrix<float>.Build.Dense(1, 1, 3.0f));
		var b = new Variable(Matrix<float>.Build.Dense(1, 1, 2.0f));
		var c = new Variable(Matrix<float>.Build.Dense(1, 1, 1.0f));
		var ys = a * b + c;
		ys.Backward();
		var expectedA = Matrix<float>.Build.Dense(1, 1, 2.0f);
		var expectedB = Matrix<float>.Build.Dense(1, 1, 3.0f);
		Assert.AreEqual(expectedA, a.Grad);
		Assert.AreEqual(expectedB, b.Grad);
	}
	
}
