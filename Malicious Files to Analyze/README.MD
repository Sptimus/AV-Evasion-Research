# Malicious Files to Analyze

This repository contains files and scripts designed for **research purposes** in analyzing **antivirus evasion techniques**. It demonstrates various methods attackers may use to bypass antivirus detection mechanisms and maintain persistence in compromised systems.

## Purpose

These files aim to demonstrate:

- **Evasion Strategies**: Techniques for bypassing antivirus detection through obfuscation, encoding, and custom payload generation.
- **Persistence Mechanisms**: Methods attackers use to maintain access to systems post-compromise, including registry modifications, task scheduling, and service creation.
- **Payload Analysis**: Comparison of detection rates for payloads generated using different tools and techniques.
- **Educational Value**: Serving as a resource for cybersecurity students, researchers, and professionals to learn about red team strategies.

---

## Included Content

1. **AdvancedPayloads**: Demonstrates advanced techniques for creating sophisticated payloads with high evasion rates.
2. **AppLockerBypass**: Explores methods for bypassing AppLocker restrictions using installation/uninstallation techniques.
3. **BasicRunner**: A basic C++ and C# shellcode loader with no additional evasion techniques.
4. **BasicShellcodeRunner**: A simple implementation of a shellcode runner used as a baseline for comparison with obfuscated versions.
5. **Cesar & CesarRunner**: Examples of payloads obfuscated using custom encoding techniques.
6. **Custom Payloads with Modifications**: Payloads demonstrating modifications such as custom encoding, sleep mechanisms, and encryption.
7. **DLLInjection**: Techniques for DLL injection, including reflective injection and advanced obfuscation.
8. **EmptyShellcodeRunner**: A simple example for demonstrating the bare-minimum shellcode execution method.
9. **Generated with Veil and Shellter**: Payloads generated using external tools like Veil and Shellter.
10. **Macros**: Malicious Office macros crafted for payload delivery.
11. **Msfvenom**: Payloads created using the Metasploit `msfvenom` tool with various evasion techniques applied.
12. **ObfuscatedBasicShellcodeRunner**: Basic shellcode runner obfuscated using tools like ConfuserEx.
13. **ProcessInjection**: Demonstrates techniques like Process Hollowing and reflective DLL injection.
14. **UAC**: Bypass User Account Control (UAC) mechanisms.
15. **XOREncoder**: Encoded shellcode using XOR techniques to evade signature-based detection.
16. **XORRunner**: Shellcode runners implementing XOR-decoded payload execution.

---

## Usage
- Use these files to test antivirus detection capabilities.
- Analyze their behavior in sandboxed environments to study evasion and persistence techniques.
