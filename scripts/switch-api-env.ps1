param (
    [Parameter(Mandatory=$true)]
    [ValidateSet("local", "dev", "auto-test", "test")]
    [string]$Environment
)

$scriptPath = Split-Path -Parent $MyInvocation.MyCommand.Path
$configPath = Join-Path $scriptPath "api-env.json"

if (-not (Test-Path $configPath)) {
    Write-Error "Configuration file not found: $configPath. Please create it based on the examples in README.md."
    exit 1
}

$config = Get-Content $configPath | ConvertFrom-Json
$envValues = $config.Environments.$Environment

if ($null -eq $envValues) {
    Write-Error "Environment '$Environment' not defined in $configPath."
    exit 1
}

$repoRoot = Split-Path -Parent $scriptPath
$apiRoot = Join-Path $repoRoot "platform\src\apis"

foreach ($target in $config.ApiTargets) {
    $settingsPath = Join-Path $apiRoot "$target\local.settings.json"
    
    if (Test-Path $settingsPath) {
        Write-Host "Updating $target..." -ForegroundColor Cyan
        $settings = Get-Content $settingsPath | ConvertFrom-Json
        
        foreach ($property in $envValues.PSObject.Properties) {
            $key = $property.Name
            if ($settings.Values.PSObject.Properties.Match($key).Count -gt 0) {
                $settings.Values.$key = $property.Value
            }
        }
        
        # Save JSON. We use standard ConvertTo-Json. 
        # Note: PS 5.1 formatting is non-standard but safe.
        $settings | ConvertTo-Json -Depth 10 | Set-Content $settingsPath -Encoding UTF8
    } else {
        Write-Warning "File not found: $settingsPath. Skipping."
    }
}

Write-Host "Successfully switched Platform API to $Environment settings." -ForegroundColor Green
