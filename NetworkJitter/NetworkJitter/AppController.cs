using System;
using System.IO;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using System.Security.Principal;

namespace NetworkJitter
{
    public partial class AppController
    {
        private double GetCurrentTime => DateTime.Now.TimeOfDay.TotalMilliseconds;

        private UserInfo GetUserInfo()
        {
            UserInfo userInfo;
            var xmlPath = Path.Combine(Directory.GetCurrentDirectory(), "settings.xml");
            if (File.Exists(xmlPath))
            {
                var serializer = new XmlSerializer(typeof(UserInfo));
                StreamReader reader = new StreamReader(xmlPath);
                userInfo = (UserInfo)serializer.Deserialize(reader);
                reader.Dispose();
            }
            else
            {
                userInfo = CreateUserInfo();
                XmlSerializer xsSubmit = new XmlSerializer(typeof(UserInfo));
                var stringWriter = new StringWriter();
                var xmlWriter = new XmlTextWriter(stringWriter);
                xmlWriter.Formatting = Formatting.Indented;
                xsSubmit.Serialize(xmlWriter, userInfo);
                var xml = stringWriter.ToString();
                File.WriteAllText(xmlPath, xml);
                xmlWriter.Dispose();
                stringWriter.Dispose();
            }
            return userInfo;
        }

        private UserInfo CreateUserInfo()
        {
            var result = new UserInfo();

            Console.Write("Enter adapter name: ");
            result.AdapterName = Console.ReadLine();
            if (string.IsNullOrEmpty(result.AdapterName) || string.IsNullOrWhiteSpace(result.AdapterName))
            {
                string defaultAdapterName = "Ethernet";
                Console.WriteLine("Adapter name set: " + defaultAdapterName);
                result.AdapterName = defaultAdapterName;
            }

            Console.Write("Enter pause (ms): ");
            string pauseTimeInput = Console.ReadLine();
            if (string.IsNullOrEmpty(pauseTimeInput) || string.IsNullOrWhiteSpace(pauseTimeInput))
            {
                int pauseTime = 3000;
                Console.WriteLine("Pause time set: " + pauseTime);
                result.PauseTime = pauseTime;
            }
            else
            {
                result.PauseTime = int.Parse(pauseTimeInput);
            }

            Console.Write("Press trigger key: ");
            result.TriggerKey = (int)Console.ReadKey().Key; 
            Console.WriteLine();

            result.SleepTime = 10; // enough
            return result;
        }

        public bool CheckAdministartorRole()
        {
            bool isElevated;
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                var principal = new WindowsPrincipal(identity);
                isElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            return isElevated;
        }

        public void Run()
        {
            Console.WriteLine("NetworkJitter v1.0");

            if (!CheckAdministartorRole())
            {
                Console.WriteLine("Run programm with Administrator role");
                Console.ReadLine();
                return;
            }

            var userInfo = GetUserInfo();

            var powerShell = new PowerShell(userInfo.AdapterName);
            double nextTime = 0f;

            while (true)
            {
                Thread.Sleep(userInfo.SleepTime);

                short triggerKeyPressed = GetAsyncKeyState(userInfo.TriggerKey);

                if (triggerKeyPressed != 0 && GetCurrentTime > nextTime)
                {
                    nextTime = GetCurrentTime + userInfo.PauseTime;
                    powerShell.SwitchIPv4(false);
                    Console.WriteLine(DateTime.Now + $" | {userInfo.AdapterName} IPv4 disabled");
                    Thread.Sleep(userInfo.PauseTime);
                    powerShell.SwitchIPv4(true);
                    Console.WriteLine(DateTime.Now + $" | {userInfo.AdapterName} IPv4 enabled");
                }
            }
        }
    }
}
