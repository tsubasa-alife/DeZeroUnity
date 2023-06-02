using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using DeZeroUnity;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

public class TestFunctions
{

    [Test]
    public void TestSquareForward()
    {
        var x = new Variable(Matrix<float>.Build.Dense(1, 1, 2.0f));
        var y = Dzf.Square(x);
        var expected = Matrix<float>.Build.Dense(1, 1, 4.0f);
        Assert.AreEqual(y.Data, expected);
    }
    
    [Test]
    public void TestSquareBackward()
    {
        var x = new Variable(Matrix<float>.Build.Dense(1, 1, 3.0f));
        var y = Dzf.Square(x);
        y.Backward();
        var expected = Matrix<float>.Build.Dense(1, 1, 6.0f);
        Assert.AreEqual(x.Grad, expected);
    }

    [Test]
    public void TestSquareGradientCheck()
    {
        var x = new Variable(Matrix<float>.Build.Random(1, 1));
        var y = Dzf.Square(x);
        y.Backward();
        var function = new Square() as Function;
        var numGrad = NumericalDiff(function, x);
        // 2つの勾配の差が小さいかどうかを確認
        var tolerance = 1e-3f;
        bool isEqual = x.Grad.AlmostEqual(numGrad, tolerance);
        Assert.IsTrue(isEqual);
    }

    /// <summary>
    /// 勾配確認のための数値微分用メソッド
    /// </summary>
    /// <param name="function"></param>
    /// <param name="x"></param>
    /// <param name="eps"></param>
    /// <returns></returns>
    private Matrix<float> NumericalDiff(Function function, Variable x, float eps = 1e-4f)
    {
        var x1 = new Variable(x.Data - eps);
        var x2 = new Variable(x.Data + eps);
        var y1 = function.Calculate(x1);
        var y2 = function.Calculate(x2);
        var diff = (y2.Data - y1.Data) / (2 * eps);
        return diff;
    }
}
