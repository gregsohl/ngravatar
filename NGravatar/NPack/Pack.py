# There is currently no exception handling in this script and it is therefore very fragile.
# All it does is zips the output directory and packages the .nuspec file.
# Remove it from the post-build event if you don't have 7-zip or NuGet installed.
# I'll try to make it more intelligent in the future.

import sys
import clr
clr.AddReference("System")
from System.IO import File, Path, Directory
from System.Diagnostics import FileVersionInfo, Process, ProcessStartInfo

def runCmd(cmd, args):
    startInfo = ProcessStartInfo()
    startInfo.FileName = cmd
    startInfo.Arguments = args
    startInfo.CreateNoWindow = True
    startInfo.UseShellExecute = False
    print "Will run " + startInfo.FileName + " " + startInfo.Arguments
    Process.Start(startInfo)
    print "Complete"

def getFileVersion(dll):
    return FileVersionInfo.GetVersionInfo(dll).FileVersion
    
def zipDllDir(dll, archLoc):
    fileVersion = getFileVersion(dll)
    path = Path.GetFullPath(dll)
    name = Path.GetFileNameWithoutExtension(path)
    dir = Path.GetDirectoryName(path)
    toDelete = Directory.GetFiles(dir, "*.tmp")
    for i in toDelete:
        File.Delete(i)
    toZip = dir + Path.DirectorySeparatorChar + "*"
    archive = archLoc + name + "." + fileVersion + ".zip"
    runCmd("7za", "a -tzip \"" + archive + "\" \"" + toZip + "\"")
    
def packNuspec(nuspec, pkgLoc):
    fileVersion = getFileVersion(dll)
    runCmd("NuGet", "pack \"" + nuspec + "\" -version " + fileVersion + " -OutputDirectory " + pkgLoc)
    
archiveLoc = sys.argv.pop()
nuspec = sys.argv.pop()
dll = sys.argv.pop()
zipDllDir(dll, archiveLoc)
packNuspec(nuspec, archiveLoc)