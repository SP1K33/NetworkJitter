using System.Runtime.InteropServices;

namespace NetworkJitter
{
	public partial class AppController
	{
		[DllImport("user32.dll")] public static extern short GetAsyncKeyState(int key);
	}
}
