using Microsoft.Win32;
using System;

class Program
{
    static void Main()
    {
        try
        {
            // Ścieżka do klucza rejestru odpowiedzialnego za autostart
            string registryPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

            // Otwieranie klucza rejestru z możliwością zapisu
            RegistryKey key = Registry.LocalMachine.CreateSubKey(registryPath);

            if (key != null)
            {
                // Dodawanie wartości do klucza rejestru, aby uruchomić C:\test.exe przy starcie systemu
                key.SetValue("TestProgram", @"C:\test.exe", RegistryValueKind.String);
                Console.WriteLine("Klucz rejestru został zmodyfikowany. Program test.exe będzie uruchamiany przy starcie systemu.");

                // Zamknięcie klucza
                key.Close();
            }
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Brak odpowiednich uprawnień do modyfikacji rejestru. Uruchom program jako administrator.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Wystąpił błąd: " + ex.Message);
        }
    }
}
