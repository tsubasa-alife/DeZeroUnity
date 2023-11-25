using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeZeroUnity
{
	public class Matrix
	{
		public int Rows { get; }

		public int Columns { get; }

		public float[,] Elements { get; }

		//コンストラクタ
		public Matrix(int row, int column)
		{
			Rows = row;
			Columns = column;
			Elements = new float[Rows, Columns];
		}

		//行列の加算
		public static Matrix operator +(Matrix a, Matrix b)
		{
			Matrix result = new Matrix(a.Rows, a.Columns);
			for (int i = 0; i < a.Rows; i++)
			{
				for (int j = 0; j < a.Columns; j++)
				{
					result.Elements[i, j] = a.Elements[i, j] + b.Elements[i, j];
				}
			}
			return result;
		}

		//行列と定数の加算(前)
		public static Matrix operator +(float b, Matrix a)
		{
			Matrix result = new Matrix(a.Rows, a.Columns);
			for (int i = 0; i < a.Rows; i++)
			{
				for (int j = 0; j < a.Columns; j++)
				{
					result.Elements[i, j] = b + a.Elements[i, j];
				}
			}
			return result;
		}

		//行列と定数の加算(後)
		public static Matrix operator +(Matrix a, float b)
		{
			Matrix result = new Matrix(a.Rows, a.Columns);
			for (int i = 0; i < a.Rows; i++)
			{
				for (int j = 0; j < a.Columns; j++)
				{
					result.Elements[i, j] = a.Elements[i, j] + b;
				}
			}
			return result;
		}

		//行列の減算
		public static Matrix operator -(Matrix a, Matrix b)
		{
			Matrix result = new Matrix(a.Rows, a.Columns);
			for (int i = 0; i < a.Rows; i++)
			{
				for (int j = 0; j < a.Columns; j++)
				{
					result.Elements[i, j] = a.Elements[i, j] - b.Elements[i, j];
				}
			}
			return result;
		}

		//行列と定数の減算(前)
		public static Matrix operator -(float b, Matrix a)
		{
			Matrix result = new Matrix(a.Rows, a.Columns);
			for (int i = 0; i < a.Rows; i++)
			{
				for (int j = 0; j < a.Columns; j++)
				{
					result.Elements[i, j] = b - a.Elements[i, j];
				}
			}
			return result;
		}

		//行列と定数の減算(後)
		public static Matrix operator -(Matrix a, float b)
		{
			Matrix result = new Matrix(a.Rows, a.Columns);
			for (int i = 0; i < a.Rows; i++)
			{
				for (int j = 0; j < a.Columns; j++)
				{
					result.Elements[i, j] = a.Elements[i, j] - b;
				}
			}
			return result;
		}

		//行列の乗算
		public static Matrix operator *(Matrix a, Matrix b)
		{
			
			if ((a.Rows == b.Rows) && (a.Columns == b.Columns))
			{
				//アダマール積(要素積)の場合
				Matrix result = new Matrix(a.Rows, a.Columns);
				for (int i = 0; i < a.Rows; i++)
				{
					for (int j = 0; j < a.Columns; j++)
					{
						result.Elements[i, j] = a.Elements[i, j] * b.Elements[i, j];
					}
				}
				return result;
				
			}
			else if (a.Columns == b.Rows)
			{
				//行列積の場合
				Matrix result = new Matrix(a.Rows, b.Columns);
				for (int i = 0; i < a.Rows; i++)
				{
					for (int j = 0; j < b.Columns; j++)
					{
						for (int k = 0; k < a.Columns; k++)
						{
							result.Elements[i, j] += a.Elements[i, k] * b.Elements[k, j];
						}
					}
				}
				return result;
			}
			else
			{
				return null;
			}
		}

		//行列の定数倍(前)
		public static Matrix operator *(float b, Matrix a)
		{
			Matrix result = new Matrix(a.Rows, a.Columns);
			for (int i = 0; i < a.Rows; i++)
			{
				for (int j = 0; j < a.Columns; j++)
				{
					result.Elements[i, j] = b * a.Elements[i, j];
				}
			}
			return result;
		}

		//行列の定数倍(後)
		public static Matrix operator *(Matrix a, float b)
		{
			Matrix result = new Matrix(a.Rows, a.Columns);
			for (int i = 0; i < a.Rows; i++)
			{
				for (int j = 0; j < a.Columns; j++)
				{
					result.Elements[i, j] = b * a.Elements[i, j];
				}
			}
			return result;
		}

		//0で初期化
		public void Zero()
		{
			for (int i = 0; i < Rows; i++)
			{
				for (int j = 0; j < Columns; j++)
				{
					Elements[i, j] = 0.0f;
				}
			}
		}

		//1で初期化
		public void One()
		{
			for (int i = 0; i < Rows; i++)
			{
				for (int j = 0; j < Columns; j++)
				{
					Elements[i, j] = 1.0f;
				}
			}
		}
		
		public Matrix transpose()
		{
			//行と列の数が反転する
			Matrix result = new Matrix(Columns,Rows);
			for (int i = 0; i < Columns; i++)
			{
				for (int j = 0; j < Rows; j++)
				{
					result.Elements[i, j] = Elements[j, i];
				}
			}
			return result;
		}

		//二乗
		public Matrix Power()
		{
			Matrix result = new Matrix(Rows,Columns);
			for (int i = 0; i < Rows; i++)
			{
				for (int j = 0; j < Columns; j++)
				{
					result.Elements[i, j] = Elements[i,j] * Elements[i,j];
				}
			}
			return result;
		}

		//表示用
		public void Show(string name="matrix")
		{
			int i = 0;
			string str = name + "\n";

			foreach(var element in Elements){
				i += 1;
				str += element;
				if((i % Columns) == 0){
					str += "\n";
				}else{
					str += " ";
				}
			}
			Debug.Log(str);
		}

	}
}