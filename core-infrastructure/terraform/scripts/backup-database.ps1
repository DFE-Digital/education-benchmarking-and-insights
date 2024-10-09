<#
    .SYNOPSIS
        Backup an Azure SQL database to Blob storage.
 
    .DESCRIPTION
        Generate a .bacpac file and push to blob storage with an expiration specified in seconds.

    .PARAMETER SubscriptionName
        The Azure Subscription name where the databases exists to backup.
 
    .PARAMETER SourceResourceGroupName
        The resource group name where the source database exists to backup.
 
    .PARAMETER KeyVaultName
        The key vault name where secrets may be resolved from.

    .PARAMETER ServerName
        The SQL Server that contain the database that you want to backup. 
 
    .PARAMETER DatabaseName
        Name of the database that should be backed up.
 
    .PARAMETER DatabaseUsernameSecret
        Key Vault secret name coresponding to the destination SQL Server administrator username.
 
    .PARAMETER DatabasePasswordSecret
        Key Vault secret name coresponding to the destination SQL Server administrator password.
 
    .PARAMETER StorageAccountName
        The StorageAccount that should hold the BACPAC file for backup. 

    .PARAMETER StorageKeySecret
        Key Vault secret name coresponding to the storage account access key.
 
    .PARAMETER ContainerName
        The StorageAccount container name that should hold the BACPAC file backup.

    .PARAMETER ExpirySeconds
        Specifies expiry period in seconds assigned to the BACPAC file stored in blob storage.
#>

Param (
    [Parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]
    [String] $SubscriptionName,
    [Parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]
    [String] $ResourceGroup,
    [Parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]
    [String] $KeyVaultName,
    [Parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]
    [String] $ServerName,
    [Parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]
    [String] $DatabaseName,
    [Parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]
    [String] $DatabaseUsernameSecret,
    [Parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]
    [String] $DatabasePasswordSecret,
    [Parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]
    [String] $StorageAccountName,
    [Parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]
    [String] $StorageKeySecret,
    [Parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]
    [String] $ContainerName,
    [Parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]
    [Int32] $ExpirySeconds
)

# Connect to Azure with system-assigned managed identity
Connect-AzAccount -Identity
Get-AzContext -ListAvailable | Where-Object { $_.Name -match $SubscriptionName } | Set-AzContext 
Get-AzContext

# Set context
$bacpacFileName = "$DatabaseName-$(Get-Date -UFormat "%Y-%m-%d_%H-%m-%S").bacpac"
$databaseUsername = Get-AzKeyVaultSecret -VaultName $KeyVaultName -Name $DatabaseUsernameSecret -AsPlainText
$databasePassword = Get-AzKeyVaultSecret -VaultName $KeyVaultName -Name $DatabasePasswordSecret
$storageUri = "https://$StorageAccountName.blob.core.windows.net/$ContainerName/$bacpacFileName"
$storageKeyType = "StorageAccessKey"
$storageKey = Get-AzKeyVaultSecret -VaultName $KeyVaultName -Name $StorageKeySecret -AsPlainText

# Export database to BACPAC
# https://learn.microsoft.com/en-us/azure/azure-sql/database/database-export?view=azuresql#powershell
$exportRequest = New-AzSqlDatabaseExport -ResourceGroupName $ResourceGroup -ServerName $ServerName -DatabaseName $DatabaseName -StorageKeyType $storageKeyType -StorageKey $storageKey -StorageUri $storageUri -AdministratorLogin $databaseUsername -AdministratorLoginPassword $databasePassword.SecretValue
if ($null -ne $exportRequest) {
    $operationStatusLink = $exportRequest.OperationStatusLink
    $operationStatusLink
    $exportStatus = Get-AzSqlDatabaseImportExportStatus -OperationStatusLink $operationStatusLink
    [Console]::Write("Exporting.")
    $lastStatusMessage = ""
    while ($exportStatus.Status -eq "InProgress") {
        Start-Sleep -s 10
        $exportStatus = Get-AzSqlDatabaseImportExportStatus -OperationStatusLink $operationStatusLink
        if ($lastStatusMessage -ne $exportStatus.StatusMessage) {
            $lastStatusMessage = $exportStatus.StatusMessage
            $progress = $lastStatusMessage.Replace("Running, Progress = ", "")
            [Console]::Write($progress)
        }
        [Console]::Write(".")
    }

    [Console]::WriteLine("")
    $exportStatus
    Write-Host "Database '$DatabaseName' is backed up. '$storageUri'"

    # Get a reference to the blob
    $context = New-AzStorageContext -StorageAccountName $StorageAccountName -StorageAccountKey $storageKey
    $blob = Get-AzStorageBlob -Context $context -Container $ContainerName -Blob $bacpacFileName

    # Set the CacheControl property to expiry period and save
    $blob.ICloudBlob.Properties.CacheControl = "max-age=$ExpirySeconds"
    $blob.ICloudBlob.SetProperties()
}
else {
    Write-Error "Could not start backup of $DatabaseName"
    exit
}