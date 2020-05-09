using System.Diagnostics;

namespace NetworkJitter
{
    public class PowerShell
    {
        private Process _powerShell;
        private string _commandInvariable;

        public PowerShell(string adapterName)
        {
            _commandInvariable = $"-NetAdapterBinding -Name '{adapterName}' -ComponentID ms_tcpip";
            _powerShell = new Process
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = "powershell",
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            _powerShell.Start();
        }

        public void SwitchIPv4(bool state)
        {
            string ipv4State = (state) ? "enable" : "disable";
            string command = ipv4State.ToString() + _commandInvariable;
            _powerShell.StandardInput.WriteLine(command);
        }
    }
}
