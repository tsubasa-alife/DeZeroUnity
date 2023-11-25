using System.Collections;
using System.Collections.Generic;

namespace DeZeroUnity.Algebra
{
	public class Matrix
	{
		public int Rows { get; }

		public int Columns { get; }

		public float[,] Elements { get; }
		
		public Matrix(int row, int column)
		{
			Rows = row;
			Columns = column;
			Elements = new float[Rows, Columns];
		}
		
		/// <summary>
		/// 0で初期化
		/// </summary>
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

		/// <summary>
		/// 1で初期化
		/// </summary>
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
		
		/// <summary>
		/// 転置行列
		/// </summary>
		/// <returns></returns>
		public Matrix Transpose()
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

		/// <summary>
		/// 二乗
		/// </summary>
		/// <returns></returns>
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

		/// <summary>
		/// 表示用メソッド
		/// </summary>
		/// <param name="name"></param>
		public string ToString(string name="matrix")
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
			
			return str;
		}
		
		/// <summary>
		/// 行列の形状が同じかどうか判定する
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool IsSameShape(Matrix a, Matrix b)
		{
			if (a.Rows == b.Rows)
			{
				return a.Columns == b.Columns;
			}
			
			return false;
		}
		
		/// <summary>
		/// 行列積が可能かどうか判定する
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool CanProduct(Matrix a, Matrix b)
		{
			return a.Columns == b.Rows;
		}
		
		/// <summary>
		/// 行列どうしの加算
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
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

		/// <summary>
		/// 行列と定数の加算(前)
		/// </summary>
		/// <param name="b"></param>
		/// <param name="a"></param>
		/// <returns></returns>
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

		/// <summary>
		/// 行列と定数の加算(後)
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
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

		/// <summary>
		/// 行列の減算
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
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

		/// <summary>
		/// 行列と定数の減算(前)
		/// </summary>
		/// <param name="b"></param>
		/// <param name="a"></param>
		/// <returns></returns>
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

		/// <summary>
		/// 行列と定数の減算(後)
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
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

		/// <summary>
		/// 行列の乗算(アダマール積、要素積)
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		public static Matrix operator *(Matrix a, Matrix b)
		{
			
			if (IsSameShape(a, b))
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
			
			if (CanProduct(a, b))
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
			
			// どちらでもない場合はエラー
			throw new System.Exception("行列の形状が不正です");
		}

		/// <summary>
		/// 行列の定数倍(前)
		/// </summary>
		/// <param name="b"></param>
		/// <param name="a"></param>
		/// <returns></returns>
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

		/// <summary>
		/// 行列の定数倍(後)
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
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


	}
}