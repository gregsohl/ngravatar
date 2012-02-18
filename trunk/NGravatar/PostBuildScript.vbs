set args = WScript.Arguments
dir = args(0)
dll = args(1)
archtarget = args(2)
archname = args(3)
nuspec = args(4)
pkgtarget = args(5)

version = getFileVersion(dll)
cmd = """C:\Program Files\7-Zip\7z.exe"" a -tzip " & archtarget & archname & "-" & version & ".zip " & dir & "*"
run cmd
cmd = """C:\Program Files\NuGet\NuGet.exe"" pack " & nuspec & " -version " & version & " -OutputDirectory " & pkgtarget
run cmd
WScript.Quit

function getFileVersion(dll)
    set fso = CreateObject("Scripting.FileSystemObject")
    version = fso.GetFileVersion(dll)
    set fso = Nothing	
	getFileVersion = version
end function

sub run(cmd)
    set shell = CreateObject("WScript.Shell")
	mycmd = "%comspec% /k " & cmd
	shell.Run cmd, 1, true
	set shell = Nothing
end sub
