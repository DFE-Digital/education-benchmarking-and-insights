# Define the base destination directory for the markdown files.
$baseDestination = "collected_markdown"

# Generate a unique datetime stamp for the subfolder.
$timestamp = Get-Date -Format "yyyyMMdd-HHmmss"
$destination = Join-Path -Path $baseDestination -ChildPath $timestamp

$ignoreFolders = @(".pytest_cache","templates","prototype","node_modules", ".git", ".idea", "dist", "bin", "obj", "collected_markdown", ".github", ".gemini", ".terraform", "terraform")

# Define a list of file names to ignore.
$ignoreFiles = @("GEMINI.md","CONTRIBUTING.md")

# Create the destination directory if it doesn't already exist.
if (-not (Test-Path -Path $destination -PathType Container)) {
    Write-Host "Creating directory: $destination"
    New-Item -ItemType Directory -Path $destination -Force
}

# Find all markdown files recursively from the current directory.
Get-ChildItem -Path . -Recurse -Filter *.md | ForEach-Object {
    # Check if the file's name is in the file ignore list.
    $isFileIgnored = $ignoreFiles -contains $_.Name

    # Split the full directory path of the file into its components.
    $pathComponents = $_.Directory.FullName.Split([System.IO.Path]::DirectorySeparatorChar, [System.IO.Path]::AltDirectorySeparatorChar)
    
    # Check if any component of the path is in the folder ignore list.
    $isFolderIgnored = $false
    foreach ($component in $pathComponents) {
        if ($ignoreFolders -contains $component) {
            $isFolderIgnored = $true
            break
        }
    }
    
    # If the file is not in an ignored directory or on the ignored files list, proceed with copying.
    if (-not $isFolderIgnored -and -not $isFileIgnored) {
        # Construct the new filename by replacing path separators with hyphens to flatten the structure.
        $relativePath = $_.FullName.Substring($PWD.Path.Length + 1)
        $newFileName = $relativePath -replace '[\\/]', '-'
        
        # Copy the file to the destination directory with its new, flattened name.
        Copy-Item -Path $_.FullName -Destination (Join-Path -Path $destination -ChildPath $newFileName)
    }
}

Write-Host "All markdown files have been copied and flattened into the '$destination' directory (ignoring specified folders and files)."
