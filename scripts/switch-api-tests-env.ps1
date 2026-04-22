param (
    [Parameter(Mandatory=$true)]
    [ValidateSet("local", "dev", "auto-test", "test")]
    [string]$Environment
)

$scriptPath = Split-Path -Parent $MyInvocation.MyCommand.Path
$configPath = Join-Path $scriptPath "api-tests-env.json"

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
$testsPath = Join-Path $repoRoot "platform\tests\Platform.ApiTests\appsettings.local.json"

if (Test-Path $testsPath) {
    Write-Host "Updating API Tests configuration..." -ForegroundColor Cyan
    $settings = Get-Content $testsPath | ConvertFrom-Json
    
    foreach ($property in $envValues.PSObject.Properties) {
        $key = $property.Name
        # Split key by ':' (e.g. "School:Host")
        $parts = $key.Split(':')
        $section = $parts[0]
        $propertyName = $parts[1]
        
        if ($null -eq $settings.$section) {
            $settings | Add-Member -MemberType NoteProperty -Name $section -Value @{}
        }
        
        $settings.$section.$propertyName = $property.Value
    }
    
    # Save JSON. We use standard ConvertTo-Json. 
    # Note: PS 5.1 formatting is non-standard but safe.
    $settings | ConvertTo-Json -Depth 10 | Set-Content $testsPath -Encoding UTF8
    Write-Host "Successfully switched API Tests to $Environment endpoints." -ForegroundColor Green
} else {
    Write-Error "File not found: $testsPath. Please create it first (you can copy appsettings.json as a template)."
    exit 1
}
