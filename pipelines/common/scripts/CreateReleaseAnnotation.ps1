# Based on: https://learn.microsoft.com/en-gb/azure/azure-monitor/app/release-and-work-item-insights?tabs=release-annotations#create-release-annotations-with-the-azure-cli
param(
    [parameter(Mandatory = $true)][string]$appInsightsResourceName,
    [parameter(Mandatory = $true)][string]$releaseName,
    [parameter(Mandatory = $false)]$releaseProperties = @()
)

# Function to ensure all Unicode characters in a JSON string are properly escaped
function Convert-UnicodeToEscapeHex {
    param (
        [parameter(Mandatory = $true)][string]$JsonString
    )
    $JsonObject = ConvertFrom-Json -InputObject $JsonString
    foreach ($property in $JsonObject.PSObject.Properties) {
        $name = $property.Name
        $value = $property.Value
        if ($value -is [string]) {
            $value = [regex]::Unescape($value)
            $OutputString = ""
            foreach ($char in $value.ToCharArray()) {
                $dec = [int]$char
                if ($dec -gt 127) {
                    $hex = [convert]::ToString($dec, 16)
                    $hex = $hex.PadLeft(4, '0')
                    $OutputString += "\u$hex"
                }
                else {
                    $OutputString += $char
                }
            }
            $JsonObject.$name = $OutputString
        }
    }
    return ConvertTo-Json -InputObject $JsonObject -Compress
}

$annotation = @{
    Id             = [GUID]::NewGuid();
    AnnotationName = $releaseName;
    EventTime      = (Get-Date).ToUniversalTime().GetDateTimeFormats("s")[0];
    Category       = "Deployment"; #Application Insights only displays annotations from the "Deployment" Category
    Properties     = ConvertTo-Json $releaseProperties -Compress
}

Write-Host "App Insights resource name:" $appInsightsResourceName

$appInsightsResourceId = az resource list --query "[?ends_with(name, '$appInsightsResourceName')].[id]" --output tsv
$appInsightsResourceExists = $appInsightsResourceId.Length -gt 0
if (!$appInsightsResourceExists) {
    Write-Error "Application Insights resource '$appInsightsResourceName' does not exist. Release annotation will not be created."
    exit
}

Write-Host "App Insights resource ID:" $appInsightsResourceId

$body = ConvertTo-Json $annotation -Compress
$body = Convert-UnicodeToEscapeHex -JsonString $body
Write-Host "PUT body: $body"

az rest --method put --uri "$($appInsightsResourceId)/Annotations?api-version=2015-05-01" --body "$($body)"
