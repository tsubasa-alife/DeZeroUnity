using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using DeZeroUnity;
using DeZeroUnity.Algebra;

public class TestBasicMath
{
	[Test]
	public void TestAddForward()
	{
		var x0 = new Variable(new Matrix(1, 1, 2.0f));
		var x1 = new Variable(new Matrix(1, 1, 3.0f));
		var ys = Dzf.Add(x0, x1);
		var expected = new Matrix(1, 1, 5.0f);
		Assert.AreEqual(expected.Elements, ys[0].Data.Elements);
	}
    
	[Test]
	public void TestAddBackward()
	{
		var x0 = new Variable(new Matrix(1, 1, 2.0f));
		var x1 = new Variable(new Matrix(1, 1, 3.0f));
		var ys = Dzf.Add(x0, x1);
		ys[0].Backward();
		var expected = new Matrix(1, 1, 1.0f);
		Assert.AreEqual(expected.Elements, x0.Grad.Data.Elements);
		Assert.AreEqual(expected.Elements, x1.Grad.Data.Elements);
	}
    
	[Test]
	public void TestAddSameBackward()
	{
		var x0 = new Variable(new Matrix(1, 1, 2.0f));
		var ys = Dzf.Add(x0, x0);
		ys[0].Backward();
		var expected = new Matrix(1, 1, 2.0f);
		Assert.AreEqual(expected.Elements, x0.Grad.Data.Elements);
	}
	
	[Test]
	public void TestMulForward()
	{
		var x0 = new Variable(new Matrix(1, 1, 2.0f));
		var x1 = new Variable(new Matrix(1, 1, 3.0f));
		var ys = Dzf.Mul(x0, x1);
		var expected = new Matrix(1, 1, 6.0f);
		Assert.AreEqual(expected.Elements, ys[0].Data.Elements);
	}
	
	[Test]
	public void TestMulBackward()
	{
		var x0 = new Variable(new Matrix(1, 1, 3.0f));
		var x1 = new Variable(new Matrix(1, 1, 2.0f));
		var ys = Dzf.Mul(x0, x1);
		ys[0].Backward();
		var expected0 = new Matrix(1, 1, 2.0f);
		var expected1 = new Matrix(1, 1, 3.0f);
		Assert.AreEqual(expected0.Elements, x0.Grad.Data.Elements);
		Assert.AreEqual(expected1.Elements, x1.Grad.Data.Elements);
	}
	
	[Test]
	public void TestOverLoad()
	{
		var a = new Variable(new Matrix(1, 1, 3.0f));
		var b = new Variable(new Matrix(1, 1, 2.0f));
		var c = new Variable(new Matrix(1, 1, 1.0f));
		var ys = a * b + c;
		ys.Backward();
		var expectedA = new Matrix(1, 1, 2.0f);
		var expectedB = new Matrix(1, 1, 3.0f);
		Assert.AreEqual(expectedA.Elements, a.Grad.Data.Elements);
		Assert.AreEqual(expectedB.Elements, b.Grad.Data.Elements);
	}
	
}
