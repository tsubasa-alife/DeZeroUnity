using System;

namespace DeZeroUnity
{
	/// <summary>
	/// モード設定を管理するクラス
	/// </summary>
	public class Config
	{
		public static bool enableBackprop = true;
		public static bool train = true;
	}

	/// <summary>
	/// 一時的なモード設定を行うためのクラス
	/// </summary>
	public class ConfigContext : IDisposable
	{
		private readonly string _name;
		private readonly bool _oldValue;

		public ConfigContext(string name, bool value)
		{
			_name = name;
			_oldValue = (bool)typeof(Config).GetField(name).GetValue(null);
			typeof(Config).GetField(name).SetValue(null, value);
		}

		public void Dispose()
		{
			typeof(Config).GetField(_name).SetValue(null, _oldValue);
		}
	}

	public static class ConfigUtils
	{
		public static IDisposable UsingConfig(string name, bool value)
		{
			return new ConfigContext(name, value);
		}

		/// <summary>
		/// 勾配計算を行わないモードにする
		/// </summary>
		/// <returns></returns>
		public static IDisposable NoGrad()
		{
			return UsingConfig("enable_backprop", false);
		}

		/// <summary>
		/// 推論モードにする
		/// </summary>
		/// <returns></returns>
		public static IDisposable InferenceMode()
		{
			return UsingConfig("train", false);
		}
	}
}