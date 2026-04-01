# Define the base destination directory.
$baseDestination = "collected_markdown"

# Generate a unique datetime stamp for the output subfolder.
$timestamp = Get-Date -Format "yyyyMMdd-HHmmss"
$destination = Join-Path -Path $baseDestination -ChildPath $timestamp

# --- Configuration ---

# 1. Folders to Merge: Markdown files within these paths (and their sub-paths) will be merged.
$foldersToMerge = @(
    "documentation/developers",
    "documentation/architecture",
    "documentation/features",
    "documentation/quality-assurance",
    "documentation/operational",
    "documentation/data",
    "documentation/guides/cfr-file-generation",
    "documentation/guides/monthly-reporting",
    "documentation/guides/chart-principles",
    "documentation/guides/chart-development-workflow"
)

# 2. Folders to Ignore: Any folder with these names will be completely ignored.
$ignoreFolders = @(".pytest_cache", "templates", "prototype", "node_modules", ".git", ".idea", "dist", "bin", "obj", "collected_markdown", ".github", ".gemini", ".terraform", "terraform")

# 3. Files to Ignore: Any markdown file with these names will be ignored.
$ignoreFiles = @("CONTRIBUTING.md", "GEMINI.md")

# --- Script Execution ---

# Create the destination directory.
if (-not (Test-Path -Path $destination -PathType Container)) {
    Write-Host "Creating directory: $destination"
    New-Item -ItemType Directory -Path $destination -Force
}

# Prepare a hashtable to hold files designated for merging.
$mergeTasks = @{}
$resolvedMergePaths = @{}
foreach ($folder in $foldersToMerge) {
    if (Test-Path -Path $folder) {
        $fullPath = (Resolve-Path -LiteralPath $folder).Path
        $mergeTasks[$fullPath] = [System.Collections.Generic.List[System.IO.FileInfo]]::new()
        $resolvedMergePaths[$fullPath] = $folder # Store original relative path for naming
    } else {
        Write-Warning "Merge folder not found, skipping: $folder"
    }
}

# Find and process all markdown files in the project.
Get-ChildItem -Path . -Recurse -Filter *.md | ForEach-Object {
    $file = $_
    
    # --- Ignore Logic ---
    
    # 1. Check if the file's name is in the file ignore list.
    if ($ignoreFiles -contains $file.Name) {
        return # Skip to the next file
    }

    # 2. Check if the file is within an ignored folder.
    $pathComponents = $file.Directory.FullName.Split([System.IO.Path]::DirectorySeparatorChar, [System.IO.Path]::AltDirectorySeparatorChar)
    $isFolderIgnored = $false
    foreach ($component in $pathComponents) {
        if ($ignoreFolders -contains $component) {
            $isFolderIgnored = $true
            break
        }
    }
    if ($isFolderIgnored) {
        return # Skip to the next file
    }

    # --- Categorization Logic (Merge or Copy) ---
    
    $categorizedForMerge = $false
    foreach ($mergePath in $mergeTasks.Keys) {
        if ($file.FullName.StartsWith($mergePath)) {
            $mergeTasks[$mergePath].Add($file)
            $categorizedForMerge = $true
            break
        }
    }

    # If not categorized for a merge, copy it individually.
    if (-not $categorizedForMerge) {
        $relativePath = $file.FullName.Substring($PWD.Path.Length + 1)
        $newFileName = $relativePath -replace '[\\/]', '-'
        Copy-Item -Path $file.FullName -Destination (Join-Path -Path $destination -ChildPath $newFileName)
    }
}

# --- Perform Merge Operations ---

foreach ($mergePath in $mergeTasks.Keys) {
    $filesToMerge = $mergeTasks[$mergePath]
    if ($filesToMerge.Count -eq 0) {
        continue
    }

    $originalPath = $resolvedMergePaths[$mergePath]
    $outputFileName = ($originalPath -replace '[\\/]', '-') + ".md"
    $outputPath = Join-Path -Path $destination -ChildPath $outputFileName

    Write-Host "Merging $($filesToMerge.Count) files from '$originalPath' into '$outputPath'"

    $mergedContent = [System.Collections.Generic.List[string]]::new()
    foreach ($file in $filesToMerge) {
        $mergedContent.Add("---")
        $mergedContent.Add("# Source: $($file.FullName.Substring($PWD.Path.Length + 1))")
        $mergedContent.Add("---")
        $mergedContent.Add("")
        $mergedContent.AddRange([System.IO.File]::ReadAllLines($file.FullName))
        $mergedContent.Add("")
    }

    [System.IO.File]::WriteAllLines($outputPath, $mergedContent)
}

Write-Host "Markdown processing complete. Output is in '$destination'."
