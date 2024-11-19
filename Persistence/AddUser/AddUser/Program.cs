using System;
using System.Diagnostics;
using System.Security.Principal;

class Program
{
    static void Main()
    {
        try
        {
            // Sprawdzenie, czy program działa jako administrator
            if (!IsAdministrator())
            {
                Console.WriteLine("Uruchom program jako administrator.");
                return;
            }

            // Nazwa nowego użytkownika
            string username = "NewUser";
            string password = "Password123";

            // Komenda do dodania użytkownika
            string addUserCommand = $"net user {username} {password} /add";

            // Komenda do dodania użytkownika do grupy Remote Desktop Users
            string addToGroupCommand = $"net localgroup \"Remote Desktop Users\" {username} /add";

            // Komenda do zmiany klucza rejestru, aby włączyć połączenia RDP
            string enableRDPCommand = @"reg add ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Terminal Server"" /v fDenyTSConnections /t REG_DWORD /d 0 /f";

            // Wykonywanie poszczególnych komend
            ExecuteCommand(addUserCommand, "Dodawanie użytkownika");
            ExecuteCommand(addToGroupCommand, "Przypisanie użytkownika do grupy Remote Desktop Users");
            ExecuteCommand(enableRDPCommand, "Włączanie połączeń zdalnego pulpitu");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Wystąpił błąd: " + ex.Message);
        }
    }

    // Funkcja do sprawdzenia, czy program działa jako administrator
    static bool IsAdministrator()
    {
        var identity = WindowsIdentity.GetCurrent();
        var principal = new WindowsPrincipal(identity);
        return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }

    // Funkcja do wykonywania poleceń CMD
    static void ExecuteCommand(string command, string operationDescription)
    {
        try
        {
            Console.WriteLine($"Wykonywanie: {operationDescription}");
            Console.WriteLine($"Komenda: {command}");

            // Uruchamianie procesu CMD
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c " + command,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();

            // Pobieranie wyników wykonania
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            // Wyświetlanie wyników
            if (process.ExitCode == 0)
            {
                Console.WriteLine($"{operationDescription} zakończone sukcesem.");
                Console.WriteLine(output);
            }
            else
            {
                Console.WriteLine($"{operationDescription} zakończone niepowodzeniem: {error}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas wykonywania operacji: {operationDescription}. Szczegóły: {ex.Message}");
        }
    }
}
