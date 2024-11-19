using System;
using System.Runtime.InteropServices;

class Program
{
    // Importowanie funkcji Windows API do zarządzania usługami
    [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    public static extern IntPtr OpenSCManager(string machineName, string databaseName, uint dwAccess);

    [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    public static extern IntPtr CreateService(
        IntPtr hSCManager,
        string lpServiceName,
        string lpDisplayName,
        uint dwDesiredAccess,
        uint dwServiceType,
        uint dwStartType,
        uint dwErrorControl,
        string lpBinaryPathName,
        string lpLoadOrderGroup,
        IntPtr lpdwTagId,
        string lpDependencies,
        string lpServiceStartName,
        string lpPassword);

    [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    public static extern bool CloseServiceHandle(IntPtr hSCObject);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern uint GetLastError();

    // Stałe używane w API SCM
    private const uint SC_MANAGER_CREATE_SERVICE = 0x0002;
    private const uint SERVICE_WIN32_OWN_PROCESS = 0x00000010;
    private const uint SERVICE_DEMAND_START = 0x00000003; // Usługa uruchamiana na żądanie
    private const uint SERVICE_ERROR_NORMAL = 0x00000001;
    private const uint SERVICE_ALL_ACCESS = 0xF01FF;

    static void Main(string[] args)
    {
        string serviceName = "MyPersistentService";
        string serviceDisplayName = "My Persistent Service";
        string serviceExecutable = @"C:\test.exe"; // Ścieżka do pliku wykonywalnego

        // Otwieranie menedżera usług
        IntPtr scmHandle = OpenSCManager(null, null, SC_MANAGER_CREATE_SERVICE);
        if (scmHandle == IntPtr.Zero)
        {
            Console.WriteLine($"Nie udało się otworzyć SCM. Kod błędu: {GetLastError()}");
            return;
        }

        // Tworzenie nowej usługi
        IntPtr serviceHandle = CreateService(
            scmHandle,
            serviceName,
            serviceDisplayName,
            SERVICE_ALL_ACCESS,
            SERVICE_WIN32_OWN_PROCESS,
            SERVICE_DEMAND_START,
            SERVICE_ERROR_NORMAL,
            serviceExecutable,
            null,
            IntPtr.Zero,
            null,
            null,
            null
        );

        if (serviceHandle == IntPtr.Zero)
        {
            Console.WriteLine($"Nie udało się utworzyć usługi. Kod błędu: {GetLastError()}");
        }
        else
        {
            Console.WriteLine("Usługa została pomyślnie utworzona.");
            CloseServiceHandle(serviceHandle);
        }

        // Zamknięcie uchwytu SCM
        CloseServiceHandle(scmHandle);
    }
}
