using System;
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
        var ys = Dzf.Square(x);
        var expected = Matrix<float>.Build.Dense(1, 1, 4.0f);
        Assert.AreEqual(expected, ys[0].Data);
    }
    
    [Test]
    public void TestSquareBackward()
    {
        var x = new Variable(Matrix<float>.Build.Dense(1, 1, 3.0f));
        var ys = Dzf.Square(x);
        ys[0].Backward();
        var expected = Matrix<float>.Build.Dense(1, 1, 6.0f);
        Assert.AreEqual(expected, x.Grad.Data);
    }

    [Test]
    public void TestSquareGradientCheck()
    {
        var x = new Variable(Matrix<float>.Build.Random(1, 1));
        var ys = Dzf.Square(x);
        ys[0].Backward();
        var function = new Square() as Function;
        var numGrad = NumericalDiff(function, x);
        // 2つの勾配の差が小さいかどうかを確認
        var tolerance = 1e-2f;
        bool isEqual = x.Grad.Data.AlmostEqual(numGrad, tolerance);
        Assert.IsTrue(isEqual);
    }
    
    [Test]
    public void TestExpForward()
    {
        var x = new Variable(Matrix<float>.Build.Dense(1, 1, 2.0f));
        var ys = Dzf.Exp(x);
        var expected = Matrix<float>.Build.Dense(1, 1, 7.389056f);
        Assert.AreEqual(expected, ys[0].Data);
    }
    
    [Test]
    public void TestExpBackward()
    {
        var x = new Variable(Matrix<float>.Build.Dense(1, 1, 3.0f));
        var ys = Dzf.Exp(x);
        ys[0].Backward();
        var expected = Matrix<float>.Build.Dense(1, 1, 20.085537f);
        Assert.AreEqual(expected, x.Grad.Data);
    }
    
    [Test]
    public void TestExpGradientCheck()
    {
        var x = new Variable(Matrix<float>.Build.Random(1, 1));
        var ys = Dzf.Exp(x);
        ys[0].Backward();
        var function = new Exp() as Function;
        var numGrad = NumericalDiff(function, x);
        // 2つの勾配の差が小さいかどうかを確認
        var tolerance = 1e-2f;
        bool isEqual = x.Grad.Data.AlmostEqual(numGrad, tolerance);
        Assert.IsTrue(isEqual);
    }

    [Test]
    public void TestComplexBackward()
    {
        var x = new Variable(Matrix<float>.Build.Dense(1, 1, 2.0f));
        var a = Dzf.Square(x);
        var b = Dzf.Square(a[0]);
        var c = Dzf.Square(a[0]);
        var ys = Dzf.Add(b[0], c[0]);
        ys[0].Backward();
        var expected = Matrix<float>.Build.Dense(1, 1, 64.0f);
        Assert.AreEqual(expected, x.Grad.Data);
    }

    [Test]
    public void TestSphereBackward()
    {
        var x = new Variable(Matrix<float>.Build.Dense(1, 1, 1.0f));
        var y = new Variable(Matrix<float>.Build.Dense(1, 1, 1.0f));
        var z = (x ^ 2) + (y ^ 2);
        z.Backward();
        var expected = Matrix<float>.Build.Dense(1, 1, 2.0f);
        Assert.AreEqual(expected, x.Grad.Data);
        Assert.AreEqual(expected, y.Grad.Data);
    }
    
    [Test]
    // ReSharper disable once IdentifierTypo
    public void TestMatyas()
    {
        var x = new Variable(Matrix<float>.Build.Dense(1, 1, 1.0f));
        var y = new Variable(Matrix<float>.Build.Dense(1, 1, 1.0f));
        var z = 0.26f * ((x ^ 2) + (y ^ 2)) - 0.48f * x * y;
        z.Backward();
        var expectedX = Matrix<float>.Build.Dense(1, 1, 0.04000002f);
        var expectedY = Matrix<float>.Build.Dense(1, 1, 0.04000002f);
        // 2つの勾配の差が小さいかどうかを確認
        var tolerance = 1e-4f;
        bool isEqualX = x.Grad.Data.AlmostEqual(expectedX, tolerance);
        bool isEqualY = y.Grad.Data.AlmostEqual(expectedY, tolerance);
        Assert.IsTrue(isEqualX);
        Assert.IsTrue(isEqualY);
    }

    [Test]
    public void TestGoldStein()
    {
        var x = new Variable(Matrix<float>.Build.Dense(1, 1, 1.0f));
        var y = new Variable(Matrix<float>.Build.Dense(1, 1, 1.0f));
        var z = (1 + ((x + y + 1) ^ 2) * (19 - 14 * x + 3 * (x ^ 2) - 14 * y + 6 * x * y + 3 * (y ^ 2))) *
                (30 + ((2 * x - 3 * y) ^ 2) * (18 - 32 * x + 12 * (x ^ 2) + 48 * y - 36 * x * y + 27 * (y ^ 2)));
        z.Backward();
        var expectedX = Matrix<float>.Build.Dense(1, 1, -5376.0f);
        var expectedY = Matrix<float>.Build.Dense(1, 1, 8064.0f);
        // 2つの勾配の差が小さいかどうかを確認
        var tolerance = 1e-4f;
        bool isEqualX = x.Grad.Data.AlmostEqual(expectedX, tolerance);
        bool isEqualY = y.Grad.Data.AlmostEqual(expectedY, tolerance);
        Assert.IsTrue(isEqualX);
        Assert.IsTrue(isEqualY);
    }

    [Test]
    public void TestSecondDerivative()
    {
        var x = new Variable(Matrix<float>.Build.Dense(1, 1, 2.0f));
        var y = (x ^ 4) - (2 * (x ^ 2));
        y.Backward(false, true);
        var expected = Matrix<float>.Build.Dense(1, 1, 24.0f);
        Assert.AreEqual(expected, x.Grad.Data);
        var gx = x.Grad;
        x.ClearGrads();
        gx.Backward();
        var expected2 = Matrix<float>.Build.Dense(1, 1, 44.0f);
        Assert.AreEqual(expected2, x.Grad.Data);
    }

    [Test]
    public void TestReshape()
    {
        var x = new Variable(Matrix<float>.Build.DenseOfArray(new float[,] { { 1, 2, 3 }, { 4, 5, 6 } }));
        var y = Dzf.Reshape(x, new Tuple<int, int>(6, 1));
        y[0].Backward();
        var expectedY = Matrix<float>.Build.Dense(6, 1, new float[] { 1, 2, 3, 4, 5, 6 });
        Assert.AreEqual(expectedY, y[0].Data);
        var expected = Matrix<float>.Build.Dense(2, 3, new float[] { 1, 1, 1, 1, 1, 1 });
        Assert.AreEqual(expected, x.Grad.Data);
    }
    
    [Test]
    public void TestTranspose()
    {
        var x = new Variable(Matrix<float>.Build.DenseOfArray(new float[,] { { 1, 2, 3 }, { 4, 5, 6 } }));
        var y = Dzf.Transpose(x);
        y[0].Backward();
        var expectedY = Matrix<float>.Build.Dense(3, 2, new float[] { 1, 2, 3, 4, 5, 6 });
        Assert.AreEqual(expectedY, y[0].Data);
        var expected = Matrix<float>.Build.Dense(2, 3, new float[] { 1, 1, 1, 1, 1, 1 });
        Assert.AreEqual(expected, x.Grad.Data);
    }

    [Test]
    public void TestSum()
    {
        var x = new Variable(Matrix<float>.Build.DenseOfArray(new float[,] { { 1, 2, 3 }, { 4, 5, 6 } }));
        var y = Dzf.Sum(x, 0);
        y[0].Backward();
        var expectedY = Matrix<float>.Build.Dense(1, 3, new float[] { 5, 7, 9 });
        Assert.AreEqual(expectedY, y[0].Data);
        var expected = Matrix<float>.Build.Dense(2, 3, new float[] { 1, 1, 1, 1, 1, 1 });
        Assert.AreEqual(expected, x.Grad.Data);
    }
    
    [Test]
    public void TestMatMul()
    {
        var x = new Variable(Matrix<float>.Build.DenseOfArray(new float[,] { { 1, 2, 3 }, { 4, 5, 6 } }));
        var w = new Variable(Matrix<float>.Build.DenseOfArray(new float[,] { { 1, 4 }, { 2, 5 }, { 3, 6 } }));
        var y = Dzf.MatMul(x, w);
        y[0].Backward();
        var expectedY = Matrix<float>.Build.DenseOfArray(new float[,] { { 14, 32 }, { 32, 77 } });
        Assert.AreEqual(expectedY, y[0].Data);
        var expectedX = Matrix<float>.Build.DenseOfArray(new float[,] { { 5, 7, 9 }, { 5, 7, 9 } });
        Assert.AreEqual(expectedX, x.Grad.Data);
        var expectedW = Matrix<float>.Build.DenseOfArray(new float[,] { { 5, 5 }, { 7, 7 }, { 9, 9 } });
        Assert.AreEqual(expectedW, w.Grad.Data);
    }

    [Test]
    public void TestAddBroadcast()
    {
        var x0 = new Variable(Matrix<float>.Build.Dense(1, 3, new float[] { 1, 2, 3 }));
        var x1 = new Variable(Matrix<float>.Build.Dense(1, 1, 10));
        var y = x0 + x1;
        var expectedY = Matrix<float>.Build.Dense(1, 3, new float[] { 11, 12, 13 });
        Assert.AreEqual(expectedY, y.Data);
        y.Backward();
        var expectedX1 = Matrix<float>.Build.Dense(1, 1, 3);
        Assert.AreEqual(expectedX1, x1.Grad.Data);
    }
    
    [Test]
    public void TestMulBroadcast()
    {
        var x0 = new Variable(Matrix<float>.Build.Dense(1, 3, new float[] { 1, 2, 3 }));
        var x1 = new Variable(Matrix<float>.Build.Dense(1, 1, 10));
        var y = x0 * x1;
        var expectedY = Matrix<float>.Build.Dense(1, 3, new float[] { 10, 20, 30 });
        Assert.AreEqual(expectedY, y.Data);
        y.Backward();
        var expectedX1 = Matrix<float>.Build.Dense(1, 1, 6);
        Assert.AreEqual(expectedX1, x1.Grad.Data);
    }

    [Test]
    public void TestSubBroadcast()
    {
        var x0 = new Variable(Matrix<float>.Build.Dense(1, 3, new float[] { 1, 2, 3 }));
        var x1 = new Variable(Matrix<float>.Build.Dense(1, 1, 10));
        var y = x0 - x1;
        var expectedY = Matrix<float>.Build.Dense(1, 3, new float[] { -9, -8, -7 });
        Assert.AreEqual(expectedY, y.Data);
        y.Backward();
        var expectedX1 = Matrix<float>.Build.Dense(1, 1, -3);
        Assert.AreEqual(expectedX1, x1.Grad.Data);
    }
    
    [Test]
    public void TestDivBroadcast()
    {
        var x0 = new Variable(Matrix<float>.Build.Dense(1, 3, new float[] { 1, 2, 3 }));
        var x1 = new Variable(Matrix<float>.Build.Dense(1, 1, 10));
        var y = x0 / x1;
        var expectedY = Matrix<float>.Build.Dense(1, 3, new float[] { 0.1f, 0.2f, 0.3f });
        Assert.AreEqual(expectedY, y.Data);
        y.Backward();
        var expectedX1 = Matrix<float>.Build.Dense(1, 1, -0.06f);
        Assert.AreEqual(expectedX1, x1.Grad.Data);
    }

    /// <summary>
    /// 勾配確認のための数値微分用メソッド
    /// </summary>
    /// <param name="function"></param>
    /// <param name="x"></param>
    /// <param name="eps"></param>
    /// <returns></returns>
    private Matrix<float> NumericalDiff(Function function, Variable x, float eps = 1e-2f)
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
