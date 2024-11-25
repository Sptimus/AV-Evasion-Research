# Custom Payloads with Modifications

This folder contains customized payloads that demonstrate various techniques for enhancing stealth and reducing antivirus detection rates. The files in this directory have undergone modifications such as obfuscation, digital signing, and compression to test their effectiveness against modern security measures.

---

## Included Files

1. **BasicRunnerCPlusUPX.exe**
   - A payload compiled in C++ and compressed using UPX to reduce detection rates.
   - **Purpose**: To explore how UPX compression impacts antivirus evasion.

2. **BasicRunnerCPlusUPXSigned.exe**
   - The same C++ payload as above, digitally signed for added legitimacy.
   - **Purpose**: Demonstrates how signing can further decrease detection.

3. **BasicShellcodeRunnerObfuscatedSigned.exe**
   - A shellcode runner that combines obfuscation and digital signing.
   - **Purpose**: To test the combined impact of obfuscation and signing on detection rates.

4. **DLLInjectionObfuscated.exe**
   - A DLL injection payload with obfuscation applied.
   - **Purpose**: Showcases how obfuscation can obscure injection behavior from antivirus systems.

5. **DLLInjectionObfuscatedSigned.exe**
   - A digitally signed version of the obfuscated DLL injection payload.
   - **Purpose**: Demonstrates how signing impacts detection of obfuscated injection techniques.

6. **ProcessHollowingObfuscated.exe**
   - A process hollowing payload with obfuscation applied to its codebase.
   - **Purpose**: Explores the role of obfuscation in avoiding detection for this technique.

7. **ProcessHollowingObfuscatedSigned.exe**
   - A signed version of the obfuscated process hollowing payload.
   - **Purpose**: Investigates the effectiveness of signing when combined with obfuscation.

8. **ProcessInjectionObfuscated.exe**
   - A payload implementing process injection, with obfuscation to mask its behavior.
   - **Purpose**: Tests the impact of obfuscation on process injection detection.

9. **ProcessInjectionObfuscatedSigned.exe**
   - A digitally signed version of the obfuscated process injection payload.
   - **Purpose**: Demonstrates how combining obfuscation with signing affects detection.

---

## Purpose

The payloads in this directory aim to:

- Analyze the effectiveness of **code obfuscation** in evading antivirus detection.
- Evaluate the impact of **digital signing** on reducing detection rates.
- Examine how combining techniques like **obfuscation and signing** improves payload stealth.
- Test the effectiveness of compression (e.g., UPX) on detection.

---

## Usage

1. **Testing**:
   - Upload the payloads to antivirus scanning platforms like VirusTotal to observe detection rates.
   - Compare the results of obfuscated, signed, and unmodified payloads.

2. **Recompiling and Modifying**:
   - The source code for these payloads is available in the repository's relevant sections.
   - Modify and rebuild the payloads using tools like Visual Studio to test your own configurations.

3. **Digital Signing**:
   - Use `signtool.exe` along with the included certificates to sign the payloads.
   - Example:
     ```bash
     signtool sign /f <certificate.pfx> /p <password> /t http://timestamp.digicert.com <file>
     ```

---

## Disclaimer

These payloads are provided for **educational and research purposes only**. The authors do not condone or support the use of these files for malicious purposes. Use responsibly and only in controlled, authorized environments.

---

## Contact

For questions, feedback, or collaboration, please raise an issue or contact the repository maintainer.
