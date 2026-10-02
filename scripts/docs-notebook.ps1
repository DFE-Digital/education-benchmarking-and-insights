param (
    [string]$SourceDir = "docs/content",
    [string]$OutputDir = ".notebook"
)

# Ensure the output directory exists
if (-not (Test-Path $OutputDir)) {
    New-Item -ItemType Directory -Path $OutputDir | Out-Null
}

$sourceFullPath = (Resolve-Path $SourceDir).Path

# Gather all subdirectories recursively, ignoring 'assets'
$directories = Get-ChildItem -Path $SourceDir -Recurse -Directory | Where-Object {
    $_.FullName -notmatch "[\/\\]assets([\/\\]|$)"
}

foreach ($dir in $directories) {
    # Find all Markdown files directly in this specific directory
    $rawFiles = Get-ChildItem -Path $dir.FullName -Filter "*.md" -File
    if ($rawFiles.Count -eq 0) { continue }

    # Extract metadata to sort files correctly
    $enrichedFiles = foreach ($file in $rawFiles) {
        $isIndex = $file.Name -eq "index.md"
        $order = 99999 # Default high order for files without an explicit order

        $content = Get-Content $file.FullName -Raw
        if ($content -match '(?s)^---\r?\n(.*?)\r?\n---\r?\n') {
            $frontmatter = $matches[1]
            if ($frontmatter -match '(?m)^order:\s*(\d+)\s*$') {
                $order = [int]$matches[1]
            }
        }

        [PSCustomObject]@{
            File    = $file
            IsIndex = $isIndex
            Order   = $order
            Name    = $file.Name
        }
    }

    # Sort: index.md always first, followed by explicit order, then alphabetically by name
    $sortedFiles = $enrichedFiles | Sort-Object @{Expression="IsIndex"; Descending=$true}, @{Expression="Order"; Ascending=$true}, @{Expression="Name"; Ascending=$true}

    # Calculate the output filename based on the relative path from docs/content
    $relPath = $dir.FullName.Substring($sourceFullPath.Length).Trim('\').Trim('/')
    if ([string]::IsNullOrEmpty($relPath)) {
        $outName = "content.md"
    } else {
        # Replace directory separators with dashes
        $outName = $relPath -replace '[\\/]', '-'
        $outName = "$outName.md"
    }

    $outPath = Join-Path $OutputDir $outName
    $contentBuilder = [System.Text.StringBuilder]::new()

    foreach ($item in $sortedFiles) {
        $file = $item.File
        $content = Get-Content $file.FullName -Raw

        $title = ""
        # Match and remove the YAML frontmatter block
        if ($content -match '(?s)^---\r?\n(.*?)\r?\n---\r?\n(.*)$') {
            $frontmatter = $matches[1]
            $body = $matches[2]

            # Extract the title from frontmatter keys
            if ($frontmatter -match '(?m)^title:\s*(.*?)\s*$') {
                $title = $matches[1] -replace '^"|"$', '' -replace "^'|'$", ''
            }
            $content = $body
        }

        # Add the title as an H1 heading if present
        if (-not [string]::IsNullOrEmpty($title)) {
            [void]$contentBuilder.AppendLine("# $title")
            [void]$contentBuilder.AppendLine()
        }

        # Append the body content, trimming any trailing/leading whitespace
        [void]$contentBuilder.AppendLine($content.Trim())
        [void]$contentBuilder.AppendLine()
    }

    # Save the flattened content as UTF-8
    Set-Content -Path $outPath -Value $contentBuilder.ToString().TrimEnd() -Encoding UTF8
    Write-Host "Successfully generated: $outPath"
}
