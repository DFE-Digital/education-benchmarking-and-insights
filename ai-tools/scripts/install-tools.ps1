$geminiDir = Join-Path $PSScriptRoot "..\..\.gemini"
$aiToolsDir = Join-Path $PSScriptRoot ".."

# Create .gemini structure if it doesn't exist
$folders = "commands", "instructions", "templates"
foreach ($folder in $folders) {
    $target = Join-Path $geminiDir $folder
    if (-not (Test-Path $target)) {
        New-Item -ItemType Directory -Path $target -Force | Out-Null
    }
}

# Copy files
Write-Host "Installing Gemini AI Tools..." -ForegroundColor Cyan

Copy-Item -Path (Join-Path $aiToolsDir "commands\*.toml") -Destination (Join-Path $geminiDir "commands") -Force
Copy-Item -Path (Join-Path $aiToolsDir "instructions\*.md") -Destination (Join-Path $geminiDir "instructions") -Force
Copy-Item -Path (Join-Path $aiToolsDir "templates\*.md") -Destination (Join-Path $geminiDir "templates") -Force

Write-Host "Success! Commands and instructions installed to $geminiDir" -ForegroundColor Green
Write-Host "You can now use commands like: /api-test-plan School Search" -ForegroundColor Cyan
