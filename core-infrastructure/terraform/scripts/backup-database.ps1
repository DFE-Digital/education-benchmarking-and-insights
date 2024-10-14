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

# Set context
$bacpacFileName = "$DatabaseName-$(Get-Date -UFormat "%Y-%m-%d_%H-%m-%S").bacpac"
$databaseUsername = Get-AzKeyVaultSecret -VaultName $KeyVaultName -Name $DatabaseUsernameSecret -AsPlainText
$databasePassword = Get-AzKeyVaultSecret -VaultName $KeyVaultName -Name $DatabasePasswordSecret -AsPlainText
$storageKey = Get-AzKeyVaultSecret -VaultName $KeyVaultName -Name $StorageKeySecret -AsPlainText

# Export database to BACPAC
dotnet new tool-manifest
dotnet tool install microsoft.sqlpackage --version 161.8089.0 # targets a version of .NET 6 that is installed on the automation container
dotnet tool run sqlpackage /Action:Export /TargetFile:"$env:temp\$bacpacFileName" /SourceConnectionString:"Server=tcp:$ServerName.database.windows.net,1433;Initial Catalog=$DatabaseName;Persist Security Info=False;User ID=$databaseUsername;Password=$databasePassword;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

# Push to blob storage
$storageContext = New-AzStorageContext -StorageAccountName $StorageAccountName -StorageAccountKey $storageKey -Protocol Https
$properties = @{ "CacheControl" = "max-age=$ExpirySeconds" }
Set-AzStorageBlobContent -File "$env:temp\$bacpacFileName" -Container $ContainerName -Blob $bacpacFileName -Properties $properties -Context $storageContext