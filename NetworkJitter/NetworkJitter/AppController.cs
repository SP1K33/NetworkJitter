using System;
using System.Threading;

namespace NetworkJitter
{
    public partial class AppController
    {
        private static PowerShell _powerShell;

        private string GetAdapterName()
        {
            Console.Write("Enter adapter name: ");
            string userInput = Console.ReadLine();
            string result = string.IsNullOrEmpty(userInput) ? "Ethernet" : userInput;
            Console.WriteLine($"Adapter name: {result}");
            return result;
        }

        private int GetPauseTime()
        {
            Console.Write("Enter pause (ms): ");
            string userInput = Console.ReadLine();
            int result = (string.IsNullOrEmpty(userInput)) ? 3000 : int.Parse(userInput);
            Console.WriteLine($"Pause time: {result}");
            return result;
        }

        private int GetPressedKey()
        {
            while (true)
            {
                for (int key = 8; key <= 255; ++key)
                {
                    if (GetAsyncKeyState(key) != 0) // HC_ACTION
                    {
                        return key;
                    }
                }
                Thread.Sleep(10);
            }
        }

        public void RunStartup()
        {
            string adapterName = GetAdapterName();
            int pauseTime = GetPauseTime();

            Console.WriteLine("Press trigger key: ");
            int triggerKey = GetPressedKey();
            Console.WriteLine($"Trigger key: {triggerKey}");

            Console.WriteLine("Press panic key: ");
            int exitKey = GetPressedKey();
            Console.WriteLine($"Panic key: {exitKey}");

            _powerShell = new PowerShell(adapterName);

            while (true)
            {
                if (GetAsyncKeyState(triggerKey) != 0x0)
                {
                    _powerShell.SwitchIPv4(false);
                    Thread.Sleep(pauseTime);
                    _powerShell.SwitchIPv4(true);
                }

                if (GetAsyncKeyState(exitKey) != 0x0)
                {
                    break;
                }
                Thread.Sleep(10);
            }
        }
    }
}
