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
        var xs = new List<Variable> { x };
        var ys = Dzf.Square(xs);
        var expected = Matrix<float>.Build.Dense(1, 1, 4.0f);
        Assert.AreEqual(ys[0].Data, expected);
    }
    
    [Test]
    public void TestSquareBackward()
    {
        var x = new Variable(Matrix<float>.Build.Dense(1, 1, 3.0f));
        var xs = new List<Variable> { x };
        var ys = Dzf.Square(xs);
        ys[0].Backward();
        var expected = Matrix<float>.Build.Dense(1, 1, 6.0f);
        Assert.AreEqual(x.Grad, expected);
    }

    [Test]
    public void TestSquareGradientCheck()
    {
        var x = new Variable(Matrix<float>.Build.Random(1, 1));
        var xs = new List<Variable> { x };
        var ys = Dzf.Square(xs);
        ys[0].Backward();
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
        var xs1 = new List<Variable> { x1 };
        var xs2 = new List<Variable> { x2 };
        var y1 = function.Calculate(xs1);
        var y2 = function.Calculate(xs2);
        var diff = (y2[0].Data - y1[0].Data) / (2 * eps);
        return diff;
    }
}
