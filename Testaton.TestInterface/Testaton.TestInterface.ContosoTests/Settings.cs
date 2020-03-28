using System;

namespace Testaton.TestInterface.ContosoTests
{
	public class Settings
	{
		private static readonly int _station = 10;

		private static readonly string _host = "ti.ath.cx";

		private static readonly string _resetUrl = "api/reset";
		private static readonly string _protocol = "http";
		
		public static string HomeAddress => _protocol + "://" + _host + "/" + _station;

		public static string ResetAddress => HomeAddress + "/" + _resetUrl;

		public static TimeSpan Timeout => TimeSpan.FromSeconds(10);
	}
}