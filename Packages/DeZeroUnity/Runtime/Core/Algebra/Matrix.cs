using System;

namespace DeZeroUnity.Algebra
{
	public class Matrix
	{
		public int Rows { get; }

		public int Columns { get; }

		public float[,] Elements { get; }
		
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public Matrix(int row, int column)
		{
			Rows = row;
			Columns = column;
			Elements = new float[Rows, Columns];
			Zero();
		}
		
		public Matrix(int row, int column, Random rand)
		{
			Rows = row;
			Columns = column;
			Elements = new float[Rows, Columns];
			NormalRandomInit(rand);
		}

		public Matrix(int row, int column, float value)
		{
			Rows = row;
			Columns = column;
			Elements = new float[Rows, Columns];
			InitWithValue(value);
		}
		
		public Matrix(float[,] elements)
		{
			Rows = elements.GetLength(0);
			Columns = elements.GetLength(1);
			Elements = elements;
		}
		
		public Matrix(int row, int column, float[] elements)
		{
			Rows = row;
			Columns = column;
			Elements = new float[Rows, Columns];
			int i = 0;
			int j = 0;
			foreach (var element in elements)
			{
				Elements[i, j] = element;
				j += 1;
				if (j == column)
				{
					j = 0;
					i += 1;
				}
			}
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
		/// 正規乱数で初期化
		/// </summary>
		public void NormalRandomInit(Random rand)
		{
			for (int i = 0; i < Rows; i++)
			{
				for (int j = 0; j < Columns ; j++)
				{
					Elements[i, j] = Distribution.NormalRandom(rand);
				}
			}
		}

		/// <summary>
		/// 特定の値で初期化
		/// </summary>
		/// <param name="value"></param>
		public void InitWithValue(float value)
		{
			for (int i = 0; i < Rows; i++)
			{
				for (int j = 0; j < Columns; j++) 
				{
					Elements[i, j] = value;
				}
			}
		}

		/// <summary>
		/// 各要素をCos変換した行列を返す
		/// </summary>
		public Matrix Cos()
		{
			Matrix result = new Matrix(Rows,Columns);
			for (int i = 0; i < Rows; i++)
			{
				for (int j = 0; j < Columns ; j++)
				{
					result.Elements[i, j] = MathF.Cos(Elements[i, j]);
				}
			}
			
			return result;
		}
		
		/// <summary>
		/// 各要素をSin変換した行列を返す
		/// </summary>
		public Matrix Sin()
		{
			Matrix result = new Matrix(Rows,Columns);
			for (int i = 0; i < Rows; i++)
			{
				for (int j = 0; j < Columns ; j++)
				{
					result.Elements[i, j] = MathF.Sin(Elements[i, j]);
				}
			}
			
			return result;
		}
		
		/// <summary>
		/// 各要素をTanh変換した行列を返す
		/// </summary>
		public Matrix Tanh()
		{
			Matrix result = new Matrix(Rows,Columns);
			for (int i = 0; i < Rows; i++)
			{
				for (int j = 0; j < Columns ; j++)
				{
					result.Elements[i, j] = MathF.Tanh(Elements[i, j]);
				}
			}
			
			return result;
		}
		
		/// <summary>
		/// 各要素をExp変換した行列を返す
		/// </summary>
		public Matrix Exp()
		{
			Matrix result = new Matrix(Rows,Columns);
			for (int i = 0; i < Rows; i++)
			{
				for (int j = 0; j < Columns ; j++)
				{
					result.Elements[i, j] = MathF.Exp(Elements[i, j]);
				}
			}

			return result;
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
		/// 累乗
		/// </summary>
		/// <returns></returns>
		public Matrix Power(float n = 2)
		{
			Matrix result = new Matrix(Rows,Columns);
			for (int i = 0; i < Rows; i++)
			{
				for (int j = 0; j < Columns ; j++)
				{
					result.Elements[i, j] = MathF.Pow(Elements[i, j], n);
				}
			}
			return result;
		}

		/// <summary>
		/// 行列の足し合わせ用メソッド
		/// </summary>
		public Matrix Sum(int axis = -1)
		{
			if (axis == -1)
			{
				// 全ての要素を足し合わせる(1×1の行列になる)
				float sum = 0.0f;
				foreach (var element in Elements)
				{
					sum += element;
				}
				Matrix result = new Matrix(1, 1)
				{
					Elements =
					{
						[0, 0] = sum
					}
				};
				return result;
			}
			
			if (axis == 0)
			{
				// 列ごとに足し合わせる
				Matrix result = new Matrix(1, Columns);
				for (int j = 0; j < Columns; j++)
				{
					for (int i = 0; i < Rows; i++)
					{
						result.Elements[0, j] += Elements[i, j];
					}
				}
				return result;
			}
			
			if (axis == 1)
			{
				// 行ごとに足し合わせる
				Matrix result = new Matrix(Rows, 1);
				for (int i = 0; i < Rows; i++)
				{
					for (int j = 0; j < Columns; j++)
					{
						result.Elements[i, 0] += Elements[i, j];
					}
				}
				return result;
			}
			
			throw new Exception("axisの値が不正です");
		}
		
		/// <summary>
		/// 特定の形状になるように足し合わせる
		/// </summary>
		/// <param name="row"></param>
		/// <param name="column"></param>
		/// <returns></returns>
		public Matrix SumTo(int row, int column)
		{
			Matrix result = new Matrix(row, column);
			if (IsSameShape(this, result))
			{
				return this;
			}

			if (row == 1 && Columns == column)
			{
				// 行を1にする
				for (int j = 0; j < column; j++)
				{
					for (int i = 0; i < Rows; i++)
					{
						result.Elements[0, j] += Elements[i, j];
					}
				}
				return result;
			}
			
			if (column == 1 && Rows == row)
			{
				// 列を1にする
				for (int i = 0; i < row; i++)
				{
					for (int j = 0; j < Columns; j++)
					{
						result.Elements[i, 0] += Elements[i, j];
					}
				}
				return result;
			}
			
			
			throw new Exception("指定の形状に足し合わせることができません");
		}

		/// <summary>
		/// ブロードキャストを行う
		/// </summary>
		public Matrix Broadcast(int row, int column)
		{
			if (Rows == 1 && Columns == 1)
			{
				Matrix result = new Matrix(row, column);
				for (int i = 0; i < row; i++)
				{
					for (int j = 0; j < column; j++)
					{
						result.Elements[i, j] = Elements[0, 0];
					}
				}
				return result;
			}
			
			if (Rows == 1 && Columns == column)
			{
				Matrix result = new Matrix(row, column);
				for (int i = 0; i < row; i++)
				{
					for (int j = 0; j < column; j++)
					{
						result.Elements[i, j] = Elements[0, j];
					}
				}
				return result;
			}
			
			if (Rows == row && Columns == 1)
			{
				Matrix result = new Matrix(row, column);
				for (int i = 0; i < row; i++)
				{
					for (int j = 0; j < column; j++)
					{
						result.Elements[i, j] = Elements[i, 0];
					}
				}
				return result;
			}
			
			throw new Exception("ブロードキャストできません" + " " + this + " " + row + " " + column);
		}
		
		/// <summary>
		/// 行列の形状を変形する
		/// </summary>
		public Matrix Reshape(int row, int column)
		{
			
			if (!HasSameNumberOfElements(this, row, column))
			{
				throw new Exception("指定の形状に変形できません");
			}
			
			Matrix result = new Matrix(row, column);
			int i = 0;
			int j = 0;
			foreach (var element in Elements)
			{
				result.Elements[i, j] = element;
				j += 1;
				if (j == column)
				{
					j = 0;
					i += 1;
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
		
		public static bool IsSameShape(Matrix a, int row, int column)
		{
			if (a.Rows == row)
			{
				return a.Columns == column;
			}
			
			return false;
		}
		
		/// <summary>
		/// 行列積が可能かどうか判定する
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool CanDotProduct(Matrix a, Matrix b)
		{
			return a.Columns == b.Rows;
		}
		
		/// <summary>
		/// 要素数が同じかどうか判定する
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool HasSameNumberOfElements(Matrix a, Matrix b)
		{
			return a.Rows * a.Columns == b.Rows * b.Columns;
		}
		
		public static bool HasSameNumberOfElements(Matrix a, int row, int column)
		{
			return a.Rows * a.Columns == row * column;
		}
		
		public static bool IsAlmostEqual(Matrix a, Matrix b, float tolerance = 0.0001f)
		{
			if (!IsSameShape(a, b))
			{
				throw new Exception("形状が異なるため比較できません");
			}
			
			for (int i = 0; i < a.Rows; i++)
			{
				for (int j = 0; j < a.Columns; j++)
				{
					if (MathF.Abs(a.Elements[i, j] - b.Elements[i, j]) > tolerance)
					{
						return false;
					}
				}
			}
			return true;
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
		/// 行列の減算(単項演算子)
		/// </summary>
		/// <param name="a"></param>
		/// <returns></returns>
		public static Matrix operator -(Matrix a)
		{
			Matrix result = new Matrix(a.Rows, a.Columns);
			for (int i = 0; i < a.Rows; i++)
			{
				for (int j = 0; j < a.Columns ; j++)
				{
					result.Elements[i, j] = -a.Elements[i, j];
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
			
			if (CanDotProduct(a, b))
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
			throw new Exception("行列の形状が不正です");
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
		
		/// <summary>
		/// 行列の割り算(アダマール積、要素積)
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public static Matrix operator /(Matrix a, Matrix b)
		{
			if (IsSameShape(a, b))
			{
				//要素ごとに割り算
				Matrix result = new Matrix(a.Rows, a.Columns);
				for (int i = 0; i < a.Rows; i++)
				{
					for (int j = 0; j < a.Columns; j++)
					{
						result.Elements[i, j] = a.Elements[i, j] / b.Elements[i, j];
					}
				}
				return result;
			}
			
			throw new Exception("行列の形状が同じではありません");
		}
		
		/// <summary>
		/// 行列の定数割り算
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static Matrix operator /(Matrix a, float b)
		{
			Matrix result = new Matrix(a.Rows, a.Columns);
			for (int i = 0; i < a.Rows; i++)
			{
				for (int j = 0; j < a.Columns; j++)
				{
					result.Elements[i, j] = a.Elements[i, j] / b;
				}
			}
			return result;
		}


	}
}