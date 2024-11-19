using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        try
        {
            // Nazwa zadania w harmonogramie
            string taskName = "TestTask";

            // Ścieżka do pliku, który ma być uruchamiany
            string appPath = @"C:\test.exe";

            // Komenda do utworzenia zadania uruchamianego przy starcie systemu
            string createTaskCommand = $@"schtasks /create /tn {taskName} /tr ""{appPath}"" /sc onlogon /f /rl highest";

            // Komenda do skonfigurowania zadania na wypadek zakończenia programu
            string createTaskOnIdleCommand = $@"schtasks /create /tn {taskName}_Idle /tr ""{appPath}"" /sc onidle /f /rl highest";

            // Wykonanie komend
            ExecuteCommand(createTaskCommand, "Dodanie zadania uruchamianego przy starcie systemu");
            ExecuteCommand(createTaskOnIdleCommand, "Dodanie zadania uruchamianego w przypadku zakończenia działania programu");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Wystąpił błąd: " + ex.Message);
        }
    }

    static void ExecuteCommand(string command, string description)
    {
        try
        {
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

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (process.ExitCode == 0)
            {
                Console.WriteLine($"{description} zakończone sukcesem.");
                Console.WriteLine(output);
            }
            else
            {
                Console.WriteLine($"{description} zakończone niepowodzeniem: {error}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas wykonywania operacji: {description}. Szczegóły: {ex.Message}");
        }
    }
}
