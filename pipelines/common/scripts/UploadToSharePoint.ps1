<#
    .SYNOPSIS
        Uploads files to SharePoint

    .PARAMETER SiteUrl
        The Url of the site collection or subsite to connect to, i.e. tenant.sharepoint.com, https://tenant.sharepoint.com, tenant.sharepoint.com/sites/hr, etc.

    .PARAMETER ClientId
        The Client ID of the Azure AD Application.

    .PARAMETER ClientSecret
        The client secret to use. When using this, technically an Azure Access Control Service (ACS) authentication will take place. This effectively means only cmdlets that are connecting to SharePoint Online will work. Cmdlets using Microsoft Graph or any other API behind the scenes will not work.

    .PARAMETER LibraryName
        Target library to where files should be uploaded.

    .PARAMETER TargetFolderPath
        Target path within library to where files should be uploaded.

    .PARAMETER SourceFolderPath
        Path containing source files to upload.
#>

Param (
    [Parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]
    [String] $SiteUrl,
    [Parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]
    [String] $ClientId,
    [Parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]
    [String] $ClientSecret,
    [Parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]
    [String] $LibraryName,
    [Parameter(Mandatory = $false)][ValidateNotNullOrEmpty()]
    [String] $TargetFolderPath,
    [Parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]
    [String] $SourceFolderPath
)

Install-Module -Name PnP.PowerShell -Force -AllowClobber -Scope CurrentUser
Import-Module PnP.PowerShell

# see: https://www.sharepointdiary.com/2019/03/sharepoint-online-migrate-folder-with-files-subfolders-using-powershell.html
$TargetFolderURL = ""
$ItemName = ""
Try {
    Write-Host "Connecting to $SiteUrl"
    Connect-PnPOnline -Url $SiteUrl -ClientId $ClientId -ClientSecret $ClientSecret -WarningAction Ignore 

    # Get the Target Folder to Upload
    Write-Host "Resolving list $LibraryName"
    $Web = Get-PnPWeb
    $List = Get-PnPList $LibraryName -Includes RootFolder
    $TargetFolder = $List.RootFolder
    $TargetFolderSiteRelativeURL = $TargetFolder.ServerRelativeURL.Replace($Web.ServerRelativeUrl, [string]::Empty)

    If ($TargetFolderPath) {
        $TargetFolderSiteRelativeURL += "/" + $TargetFolderPath.Replace("\", "/")
    }

    # Get All Items from the Source
    Write-Host "Getting source files from $SourceFolderPath"
    $Source = Get-ChildItem -Path $SourceFolderPath -Recurse
    $SourceItems = $Source | Select-Object FullName, PSIsContainer, @{Label = 'TargetItemURL'; Expression = { $_.FullName.Replace($SourceFolderPath, $TargetFolderSiteRelativeURL).Replace("\", "/") } }
    Write-Host "$($SourceItems.Count) files(s) found to upload"

    # Upload Source Items from Fileshare to Target SharePoint Online document library
    $Counter = 1
    $SourceItems | ForEach-Object {
        # Calculate Target Folder URL
        $TargetFolderURL = (Split-Path $_.TargetItemURL -Parent).Replace("\", "/")
        $ItemName = Split-Path $_.FullName -leaf
                
        # Replace Invalid Characters
        $ItemName = [RegEx]::Replace($ItemName, "[{0}]" -f ([RegEx]::Escape([String]'\*:<>?/\|')), '_')

        # Display Progress bar
        $Status = "uploading '" + $ItemName + "' to " + $TargetFolderURL + " ($($Counter) of $($SourceItems.Count))"
        Write-Progress -Activity "Uploading ..." -Status $Status -PercentComplete (($Counter / $SourceItems.Count) * 100)

        If ($_.PSIsContainer) {
            # Ensure Folder
            $null = Resolve-PnPFolder -SiteRelativePath ($TargetFolderURL + "/" + $ItemName)
            Write-Host "Ensured folder '$($ItemName)' exists at $($Web.Url)/$TargetFolderURL"
        }
        Else {
            # Upload File
            If ($TargetFolderURL.StartsWith("/")) { 
                $TargetFolderURL = $TargetFolderURL.Remove(0, 1) 
            }

            $null = Add-PnPFile -Path $_.FullName -Folder $TargetFolderURL
            Write-Host "Uploaded '$($_.FullName)' to $($Web.Url)/$TargetFolderURL"
        }
        
        $Counter++
    }
}
Catch {
    If ($ItemName) {
        Write-Host "$ItemName could not be uploaded to $($Web.Url)/$TargetFolderURL" -ForegroundColor Red
    }

    Write-Error "Unable to upload file(s): $_"
}