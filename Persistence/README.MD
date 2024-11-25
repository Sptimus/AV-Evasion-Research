# Persistence Techniques in Cybersecurity

This folder contains various files and scripts designed to demonstrate **persistence techniques** used in cybersecurity research, particularly from a **red team perspective**. These examples illustrate methods attackers might use to maintain access to a compromised system, even after reboots or user logouts.

## Included Content:

1. **AddUser**: Demonstrates how to create a new user account with administrative privileges and add it to the `Remote Desktop Users` group, enabling remote access.
2. **SCM (Service Control Manager)**: Explores persistence via the creation of custom services that start automatically during system boot.
3. **Registry**: Shows techniques to modify system registry keys (`SOFTWARE\Microsoft\Windows\CurrentVersion\Run`) to achieve persistence by launching malicious code during startup.
4. **Scheduler**: Demonstrates how to configure scheduled tasks in Windows to execute code periodically or on specific system events.
5. **Startup**: Utilizes the `Startup` folder in Windows to add malicious executables or shortcuts that launch upon user login.

---

## Purpose:

- **Education**: Provides practical examples to learn about system persistence mechanisms.
- **Testing**: Enables the analysis of defensive measures, such as antivirus and EDR (Endpoint Detection and Response), against persistence techniques.
- **Research**: Aids in understanding how attackers leverage persistence strategies to evade detection and maintain access.

---

## Usage Instructions:

1. Clone this repository:
   ```bash
   git clone https://github.com/Sptimus/AV-Evasion-Research.git
