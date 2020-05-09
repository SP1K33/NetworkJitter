using System;
using System.Threading;

namespace NetworkJitter
{
    public partial class AppController
    {
        private string GetUserInput(string warningMessage, string resultMessage)
        {
            Console.Write(warningMessage);
            string userInput = Console.ReadLine();
            Console.WriteLine(resultMessage + userInput);
            return userInput;
        }

        private int GetUserKeyDown(string warningMessage, string resultMessage)
        {
            Console.Write(warningMessage);
            var userInput = Console.ReadKey().Key;
            Console.WriteLine(resultMessage + userInput);
            return (int)userInput;
        }

        private UserInfo CollectUserInfo()
        {
            var result = new UserInfo
            {
                AdapterName = GetUserInput("Enter adapter name: ", "Adapter name: "),
                PauseTime = int.Parse(GetUserInput("Enter pause (ms): ", "Pause time: ")),
                TriggerKey = GetUserKeyDown("Press trigger key: ", "Trigger key: "),
                ExitKey = GetUserKeyDown("Press panic key: ", "Panic key: "),
                SleepTime = 10 // enough
            };
            return result;
        }

        public void RunStartup()
        {
            var userInfo = CollectUserInfo();
            var powerShell = new PowerShell(userInfo.AdapterName);

            while (true)
            {
                if (GetAsyncKeyState(userInfo.TriggerKey) != 0x0)
                {
                    powerShell.SwitchIPv4(false);
                    Thread.Sleep(userInfo.PauseTime);
                    powerShell.SwitchIPv4(true);
                }

                if (GetAsyncKeyState(69) != 0x0)
                {
                    break;
                }
                Thread.Sleep(10);
            }
        }
    }
}
