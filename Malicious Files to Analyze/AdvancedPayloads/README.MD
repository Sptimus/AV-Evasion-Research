# AdvancedPayloads

This directory contains examples of advanced payloads with additional features designed to evade antivirus detection and enhance stealth. These files demonstrate techniques such as code signing, obfuscation, and payload enhancement.

## Included Files

1. **BasicShellcodeRunnerObfuscatedSigned.exe**
   - This executable demonstrates a shellcode runner that has been obfuscated and digitally signed to reduce detection by antivirus engines.
   - **Purpose**: Combines obfuscation and digital signatures to bypass static and heuristic analysis.

2. **BasicShellcodeRunnerSigned.exe**
   - A signed version of a basic shellcode runner, showcasing how digital signatures can reduce detection rates.
   - **Purpose**: Highlights the role of code signing in creating payloads that appear legitimate.

3. **sign.pfx**
   - A sample code signing certificate used for signing the executables.
   - **Note**: For demonstration purposes only.

4. **signtool.exe**
   - The tool used to sign the payloads, illustrating the signing process.

---

## Purpose

The payloads in this folder are designed to:

- Explore the impact of **digital signing** on detection rates.
- Demonstrate the combination of **obfuscation and signing** for improved evasion.
- Serve as a resource for understanding **payload hardening techniques**.

---

## Usage

1. To run the signed executables:
   - Ensure you understand the implications of running potentially malicious payloads in a controlled environment.
   - Execute the `.exe` files in a test environment or sandbox.

2. To test detection rates:
   - Upload the files to an antivirus scanning platform like VirusTotal.
   - Observe the impact of signing and obfuscation on detection rates.

3. To replicate or modify:
   - Use `signtool.exe` with `sign.pfx` to apply signatures to other payloads.
   - Example usage:
     ```bash
     signtool sign /f sign.pfx /p <password> /t http://timestamp.digicert.com <file-to-sign>
     ```

---

## Disclaimer

This directory is for **educational and research purposes only**. Do not use these files in unauthorized environments or for malicious purposes. The authors are not responsible for misuse.

---

## Contact

For questions or suggestions, please raise an issue or contact the repository maintainer.
