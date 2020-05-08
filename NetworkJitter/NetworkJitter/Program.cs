using System;
using System.Windows.Forms;

namespace NetworkJitter
{
	public static partial class Program
	{
        [STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			AppController.Instance.RunStartup();
            Application.Run(new MainForm());
		}
	}
}
