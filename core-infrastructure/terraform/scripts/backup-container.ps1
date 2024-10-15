<#
    .SYNOPSIS
        Backup a Blob storage container to another Blob storage location.
 
    .DESCRIPTION
        Copies files from a storage container to another storage container as a timestamped backup.

    .PARAMETER SubscriptionName
        The Azure Subscription name where the storage accounts exists to backup.
 
    .PARAMETER ResourceGroup
        The resource group name where the storage accounts exists to backup.
 
    .PARAMETER KeyVaultName
        The key vault name where secrets may be resolved from.

    .PARAMETER SourceStorageAccountName
        The StorageAccount that contains the files to back up. 

    .PARAMETER SourceStorageKeySecret
        Key Vault secret name coresponding to the source storage account access key.
 
    .PARAMETER SourceContainerName
        The StorageAccount container name that contains the files to back up.
 
    .PARAMETER TargetStorageAccountName
        The StorageAccount that should hold the backed up files. 

    .PARAMETER TargetStorageKeySecret
        Key Vault secret name coresponding to the target storage account access key.
 
    .PARAMETER TargetContainerName
        The StorageAccount container name that should hold the backed up files.

    .PARAMETER TargetFolderName
        The container folder name that should hold the backed up files.

    .PARAMETER ExpirySeconds
        Specifies expiry period in seconds assigned to the files stored in blob storage.
#>

Param (
    [Parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]
    [String] $SubscriptionName,
    [Parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]
    [String] $ResourceGroup,
    [Parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]
    [String] $KeyVaultName,
    [Parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]
    [String] $SourceStorageAccountName,
    [Parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]
    [String] $SourceStorageKeySecret,
    [Parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]
    [String] $SourceContainerName,
    [Parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]
    [String] $TargetStorageAccountName,
    [Parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]
    [String] $TargetStorageKeySecret,
    [Parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]
    [String] $TargetContainerName,
    [Parameter(Mandatory = $false)][ValidateNotNullOrEmpty()]
    [Boolean] $TargetFolderTimestamp,
    [Parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]
    [Int32] $ExpirySeconds
)

# Connect to Azure with system-assigned managed identity
Connect-AzAccount -Identity

# Set context
$sourceStorageKey = Get-AzKeyVaultSecret -VaultName $KeyVaultName -Name $SourceStorageKeySecret -AsPlainText
$targetStorageKey = Get-AzKeyVaultSecret -VaultName $KeyVaultName -Name $TargetStorageKeySecret -AsPlainText
$sourceStorageContext = New-AzStorageContext -StorageAccountName $SourceStorageAccountName -StorageAccountKey $sourceStorageKey -Protocol Https
$targetStorageContext = New-AzStorageContext -StorageAccountName $TargetStorageAccountName -StorageAccountKey $targetStorageKey -Protocol Https
$timestamp = Get-Date -UFormat "%Y-%m-%d_%H-%M-%S"

# below is based on https://stackoverflow.com/a/43777258/504477
$blobs = Get-AzStorageBlob -Context $sourceStorageContext -Container $SourceContainerName
$copiedBlobs = @()
foreach ($Blob in $blobs) {
    $blobName = $Blob.Name
    if ($TargetFolderTimestamp -eq $True) {
        $targetBlobName = "$timestamp/$blobName"
    } 
    
    Write-Output "Copying $SourceContainerName/$blobName to $targetContainerName/$targetBlobName"
    $blobCopy = Start-AzStorageBlobCopy -Context $sourceStorageContext -SrcContainer $SourceContainerName -SrcBlob $blobName -DestContext $targetStorageContext -DestContainer $SourceContainerName -DestBlob $TargetBlobName
    $copiedBlobs += $blobCopy
}

# Start-AzStorageBlobCopy is asynchronous, so need to poll state until all are successful 
$completeBlobs = @()
while ($completeBlobs.Length -lt $copiedBlobs.Length) {
    Start-Sleep -Seconds 60

    foreach ($copiedBlob in $copiedBlobs) {
        [Microsoft.Azure.Storage.Blob.CopyState] $copyState

        try {
            $copyState = $copiedBlob | Get-AzStorageBlobCopyState -ErrorAction stop
        }
        catch {
            if ($completeBlobs -notcontains $copiedBlob) {
                $completeBlobs += $copiedBlob
            }

            Write-Output $("Unable to determine blob copy state of $($copiedBlob.Name). $($_.Exception.Message)")
            Write-Verbose $_.ScriptStackTrace
        }

        if ($null -ne $copyState) {
            $message = $copyState.Source.AbsolutePath + " " + $copyState.Status + " {0:N2}%" -f (($copyState.BytesCopied / $copyState.TotalBytes) * 100) 
            Write-Verbose $message
        }

        # wait until copy complete and then update the properties, otherwise content length is set to zero
        if (($copyState.BytesCopied -ge $copyState.TotalBytes) -and ($completeBlobs -notcontains $copiedBlob)) {
            $completeBlobs += $copiedBlob
            $blob = Get-AzStorageBlob -Container $targetContainerName -Blob $copiedBlob.Name -Context $targetStorageContext
            $blob.ICloudBlob.Properties.CacheControl = "max-age=$ExpirySeconds"
            $blob.ICloudBlob.SetProperties()
            Write-Verbose $("Updated properties for $($copiedBlob.Name)")
        }
    }

    Write-Output $("Completed blobs: $($completeBlobs.Length)/$($copiedBlobs.Length)")
}
