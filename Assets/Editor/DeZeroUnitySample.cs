using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;
using DeZeroUnity;
using UnityEditor;
using UnityEngine;

public class DeZeroUnitySample
{
	[MenuItem("DeZeroUnity/XORSample")]
	public static void XORSample()
	{
		const int epochs = 10000;
		float progress = 0;
		
		float lr = 0.2f;

		var model = new TwoLayerNet(2, 3, 1);
		var optimizer = new SGD(lr);
		optimizer.Setup(model);
		
		var inputData = Matrix<float>.Build.DenseOfArray(new float[,]
		{
			{0, 0},
			{1, 0},
			{0, 1},
			{1, 1}
		});
		
		var trainData = Matrix<float>.Build.DenseOfArray(new float[,]
		{
			{0},
			{1},
			{1},
			{0}
		});
		
		Debug.Log("XORSample 学習前の予測");
		
		Debug.Log("0, 0 => " + model.Forward(new Variable(Matrix<float>.Build.Dense(1, 2, new float[] {0, 0}))).Data[0,0]);
		Debug.Log("1, 0 => " + model.Forward(new Variable(Matrix<float>.Build.Dense(1, 2, new float[] {1, 0}))).Data[0,0]);
		Debug.Log("0, 1 => " + model.Forward(new Variable(Matrix<float>.Build.Dense(1, 2, new float[] {0, 1}))).Data[0,0]);
		Debug.Log("1, 1 => " + model.Forward(new Variable(Matrix<float>.Build.Dense(1, 2, new float[] {1, 1}))).Data[0,0]);
		
		Debug.Log("XORSample 学習開始");
		
		for (int i = 0; i < epochs; i++)
		{
			for (int j = 0; j < inputData.RowCount; j++)
			{
				var x = new Variable(Matrix<float>.Build.Dense(1, 2, new float[] {inputData[j, 0], inputData[j, 1]}));
				var t = new Variable(Matrix<float>.Build.Dense(1, 1, trainData[j, 0]));
				
				var y = model.Forward(x);
				var loss = Dzf.MeanSquaredError(y, t);
				
				model.ClearGrads();
				loss[0].Backward();
				
				optimizer.Update();
			}

			progress = (float)i / epochs;
			EditorUtility.DisplayProgressBar("XORSample学習中", "epoch:" + i + "(" + (progress * 100).ToString("F0") + "%)",progress);
		}
		
		EditorUtility.ClearProgressBar();
		Debug.Log("XORSample 学習完了");
		Debug.Log("XORSample 学習後の予測");
		Debug.Log("0, 0 => " + model.Forward(new Variable(Matrix<float>.Build.Dense(1, 2, new float[] {0, 0}))).Data[0,0]);
		Debug.Log("1, 0 => " + model.Forward(new Variable(Matrix<float>.Build.Dense(1, 2, new float[] {1, 0}))).Data[0,0]);
		Debug.Log("0, 1 => " + model.Forward(new Variable(Matrix<float>.Build.Dense(1, 2, new float[] {0, 1}))).Data[0,0]);
		Debug.Log("1, 1 => " + model.Forward(new Variable(Matrix<float>.Build.Dense(1, 2, new float[] {1, 1}))).Data[0,0]);

	}
	
}