# Runs every time a package is installed in a project
 
param($installPath, $toolsPath, $package, $project)
 
# $installPath is the path to the folder where the package is installed.
# $toolsPath is the path to the tools directory in the folder where the package is installed.
# $package is a reference to the package object.
# $project is a reference to the project the package was installed to.
  
function SetFilePropertiesRecursively
{
    $folderKind = "{6BB5F8EF-4483-11D3-8BCF-00C04F8EC28C}";
    foreach ($subItem in $args[0].ProjectItems)
    {
        $path = $args[1]
        if ($subItem.Name -like "*.cs" )
        {
            Write-Host -NoNewLine ("{0}{1}{2}" -f $path, $args[0].Name, "\")
            Write-Host (" Setting {0} to Compile Do not copy" -f $subItem.Name)
            SetFileProperties $subItem 1 0
            continue
        }
        if ($subItem.Name -like "*.tt" )
        {
            Write-Host -NoNewLine ("{0}{1}{2}" -f $path, $args[0].Name, "\")
            Write-Host (" Setting {0} to None Do not copy" -f $subItem.Name)
            SetFileProperties $subItem 0 0
            continue
        }
        else
        {
            Write-Host -NoNewLine ("{0}{1}{2}" -f $path, $args[0].Name, "\")
            Write-Host ("Setting {0} to Content Copy if newer" -f $subItem.Name)
            SetFileProperties $subItem 2 2
        }    
    }
}

function SetFileProperties
{
    param([System.__ComObject]$item, [int]$buildAction, [int]$copyTo)
    Write-Host "  Setting Build Action"
    $item.Properties.Item("BuildAction").Value = $buildAction
    Write-Host "  Setting Copy To Output Directory"
    $item.Properties.Item("CopyToOutputDirectory").Value = $copyTo
}
 
SetFilePropertiesRecursively $project.ProjectItems.Item("WebsiteUnderTest")

Write-Host " install.ps1 finished" 