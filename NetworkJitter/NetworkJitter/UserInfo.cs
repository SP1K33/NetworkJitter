using System;

namespace NetworkJitter
{
	public struct UserInfo
	{
		public string AdapterName { get; set; }
		public int PauseTime { get; set; }
		public int TriggerKey { get; set; }
		public int ExitKey { get; set; }
		public int SleepTime { get; set; }
	}
}
