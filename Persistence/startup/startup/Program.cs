using System;
using System.IO;
using IWshRuntimeLibrary;

class Program
{
    static void Main()
    {
        try
        {
            // Ścieżka do folderu "Startup" użytkownika
            string startupFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);

            // Ścieżka do aplikacji, którą chcemy dodać do autostartu
            string appPath = @"C:\test.exe";
            string shortcutPath = Path.Combine(startupFolderPath, "MyStartupApp.lnk");

            // Tworzenie skrótu
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
            shortcut.TargetPath = appPath;
            shortcut.WorkingDirectory = Path.GetDirectoryName(appPath);
            shortcut.Description = "My Startup Application";
            shortcut.Save();

            Console.WriteLine("Skrót został pomyślnie dodany do folderu Startup.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Wystąpił błąd: " + ex.Message);
        }
    }
}
