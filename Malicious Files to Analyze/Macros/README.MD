# Macros

This directory contains scripts and examples of macros commonly used in penetration testing and research into antivirus evasion strategies. The macros are designed to simulate real-world scenarios involving document-based attacks and to explore how security systems respond to such methods.

## Purpose

Macros in this folder are created to:
- **Test antivirus evasion:** Evaluate how different antivirus solutions detect and handle malicious macros.
- **Analyze security mechanisms:** Understand how macros interact with operating systems and security software during execution.
- **Demonstrate persistence techniques:** Showcase how macros can be used to maintain access to systems by embedding malicious logic into document files.

## Contents

- **VBA Scripts:** Visual Basic for Applications macros designed to execute payloads or alter system settings.
- **Malicious Document Examples:** Pre-configured document files embedding the macros for testing purposes.
- **Documentation:** Details on the behavior, structure, and use of each macro.


## `WordAdvancedRunner.vbs`

This script serves as a foundational runner designed to load and execute shellcode within the `WINWORD.exe` process. It features detection and patching mechanisms for AMSI (Anti-Malware Scan Interface) on both 32-bit and 64-bit systems. Additionally, it contains embedded shellcode for both architectures, dynamically determining the target system's architecture before execution.

The script employs a `sleep` function to detect and evade antivirus sandbox environments. Optionally, it can validate that the target system resides within the `192.168.0.0/16` IP range, although this functionality is commented out by default and can be activated by the user.

The shellcode in this script is presented in a non-obfuscated form, leaving obfuscation as an exercise for the user. Further obfuscation techniques could enhance stealth, but these are omitted here to maintain the script’s readability and educational value.

## `WordBasicRunner.vbs`

A simplified version of `WordAdvancedRunner.vbs` that removes advanced features such as AMSI bypass and IP range validation. It is intended for scenarios where minimal functionality is sufficient or as an introduction to the core concepts.


## Disclaimer

These macros are intended for research and educational purposes only. They should be used in controlled environments to avoid unintended consequences or violations of ethical and legal standards. Misuse of these resources is strictly prohibited. 


