using System;
using System.Diagnostics;

namespace NetworkJitter
{
    public class PowerShell : IDisposable
    {
        private Process _powerShell;
        private string _adapterName;

        public PowerShell(string adapterName)
        {
            string fileName = "powershell";
            _adapterName = adapterName;
            _powerShell = new Process
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = fileName,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = false
                }
            };
            _powerShell.Start();
        }

        public void SwitchIPv4(bool state)
        {
            string ipv4State = (state) ? "enable" : "disable";
            string command = $"{ipv4State}-NetAdapterBinding -Name '{_adapterName}' -ComponentID ms_tcpip";
            _powerShell.StandardInput.WriteLine(command);
        }

        public void Dispose()
        {
            _powerShell.Kill();
            _powerShell.Dispose();
        }
    }
}
