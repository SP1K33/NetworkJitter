using System;

namespace NetworkJitter
{
    public class AppController
    {
        #region Singleton
        private static AppController _instance;

        public static AppController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppController();
                }
                return _instance;
            }
        }
        #endregion

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

        public void RunStartup()
        {
            string adapterName = GetAdapterName();
            int pauseTime = GetPauseTime();

            _powerShell = new PowerShell(adapterName);

        }


    }
}
