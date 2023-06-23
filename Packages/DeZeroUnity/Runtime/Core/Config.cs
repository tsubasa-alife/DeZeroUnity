using System;

namespace DeZeroUnity
{
	public class Config
	{
		public static bool enableBackprop = true;
		public static bool train = true;
	}

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

		public static IDisposable NoGrad()
		{
			return UsingConfig("enable_backprop", false);
		}

		public static IDisposable TestMode()
		{
			return UsingConfig("train", false);
		}
	}
}