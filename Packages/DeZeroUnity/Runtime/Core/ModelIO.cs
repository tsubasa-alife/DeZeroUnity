using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DeZeroUnity
{
	/// <summary>
	/// モデルの保存および読み込み用のクラス
	/// </summary>
	public class ModelIO
	{
		private static string DefaultPath = "Assets/ModelBin/";
		
		public static string GetDefaultModelPath()
		{
			return DefaultPath;
		}
		
		/// <summary>
		/// パラメタをバイナリファイルに保存
		/// </summary>
		/// <param name="model"></param>
		/// <param name="filePath"></param>
		public static void Save(Model model, string filePath)
		{
			if (File.Exists(filePath))
			{
				File.Delete(filePath);
			}
			else
			{
				Directory.CreateDirectory(Path.GetDirectoryName(filePath));
			}
			
			// モデルのパラメタを一つのリストにまとめる
			var paramList = model.FlattenParams();
			
			// パラメタのリストをバイナリファイルに保存
			using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(fileStream, paramList);
			}
			
		}
		
		/// <summary>
		/// パラメタをバイナリファイルから読み込み
		/// </summary>
		/// <param name="model"></param>
		/// <param name="filePath"></param>
		public static void Load(Model model, string filePath)
		{
			List<float[,]> restoredParamsList;
			// パラメタのリストをバイナリファイルから読み込み
			using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				restoredParamsList = (List<float[,]>)binaryFormatter.Deserialize(fileStream);
			}
			
			// 読み込んだリストを順番にパラメタに設定
			foreach (var param in model.Params)
			{
				param.Data.Elements = restoredParamsList[0];
				// 読み込んだパラメタをリストから削除
				restoredParamsList.RemoveAt(0);
			}

			foreach (var layer in model.Layers)
			{
				foreach (var param in layer.Params)
				{
					param.Data.Elements = restoredParamsList[0];
					// 読み込んだパラメタをリストから削除
					restoredParamsList.RemoveAt(0);
				}
			}
		}
	}
}