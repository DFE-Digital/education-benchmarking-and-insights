using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Runtime.CompilerServices;

string GetScriptDirectory([CallerFilePath] string path = "") => Path.GetDirectoryName(path) ?? throw new InvalidOperationException("Could not resolve script directory.");
var scriptDir = GetScriptDirectory();
var ConfigFileName = Path.Combine(scriptDir, "settings.json");

if (!File.Exists(ConfigFileName))
{
    Console.WriteLine($"Error: Configuration file '{ConfigFileName}' not found.");
    Console.WriteLine("Please copy 'settings.example.json' to 'settings.json' and populate it.");
    Console.WriteLine("Usage: dotnet run scripts/markdown-tool/app.cs");
    return;
}

var configJson = File.ReadAllText(ConfigFileName);
Config config;
using (var doc = JsonDocument.Parse(configJson))
{
    var root = doc.RootElement;
    config = new Config
    {
        BaseDestination = GetProperty(root, "BaseDestination"),
        FoldersToMerge = root.TryGetProperty("FoldersToMerge", out var merge) 
            ? merge.EnumerateArray().Select(x => x.GetString() ?? "").Where(s => !string.IsNullOrEmpty(s)).ToArray() 
            : Array.Empty<string>(),
        IgnoreFolders = root.TryGetProperty("IgnoreFolders", out var ignoreFoldersProp) 
            ? ignoreFoldersProp.EnumerateArray().Select(x => x.GetString() ?? "").Where(s => !string.IsNullOrEmpty(s)).ToArray() 
            : Array.Empty<string>(),
        IgnoreFiles = root.TryGetProperty("IgnoreFiles", out var ignoreFilesProp) 
            ? ignoreFilesProp.EnumerateArray().Select(x => x.GetString() ?? "").Where(s => !string.IsNullOrEmpty(s)).ToArray() 
            : Array.Empty<string>()
    };
}

string GetProperty(JsonElement element, string name)
{
    foreach (var prop in element.EnumerateObject())
    {
        if (string.Equals(prop.Name, name, StringComparison.OrdinalIgnoreCase))
        {
            return prop.Value.GetString() ?? "";
        }
    }
    return "";
}

var repoRoot = Path.GetFullPath(Path.Combine(scriptDir, "..", ".."));
var destinationRoot = Path.Combine(repoRoot, config.BaseDestination);
var timestamp = DateTime.Now.ToString("yyyyMMdd-HHmmss");
var destination = Path.Combine(destinationRoot, timestamp);

if (!Directory.Exists(destination))
{
    Console.WriteLine($"Creating directory: {destination}");
    Directory.CreateDirectory(destination);
}

var foldersToMerge = config.FoldersToMerge.Select(f => Path.GetFullPath(Path.Combine(repoRoot, f))).ToList();
var ignoreFolders = new HashSet<string>(config.IgnoreFolders, StringComparer.OrdinalIgnoreCase);
var ignoreFiles = new HashSet<string>(config.IgnoreFiles, StringComparer.OrdinalIgnoreCase);

var mergeTasks = new Dictionary<string, List<string>>();
foreach (var folder in foldersToMerge)
{
    if (Directory.Exists(folder))
    {
        mergeTasks[folder] = new List<string>();
    }
    else
    {
        Console.WriteLine($"Warning: Merge folder not found, skipping: {folder}");
    }
}

Console.WriteLine("Scanning for markdown files...");

var allFiles = Directory.EnumerateFiles(repoRoot, "*.md", SearchOption.AllDirectories);
int processedCount = 0;
int copiedCount = 0;
int mergedCount = 0;

foreach (var filePath in allFiles)
{
    var fileName = Path.GetFileName(filePath);
    
    // 1. Ignore specific files
    if (ignoreFiles.Contains(fileName)) continue;

    // 2. Ignore specific folders
    var pathParts = filePath.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
    if (pathParts.Any(p => ignoreFolders.Contains(p))) continue;

    processedCount++;

    // 3. Categorize for merge or copy
    string? matchedMergeFolder = mergeTasks.Keys.FirstOrDefault(k => filePath.StartsWith(k, StringComparison.OrdinalIgnoreCase));

    if (matchedMergeFolder != null)
    {
        mergeTasks[matchedMergeFolder].Add(filePath);
    }
    else
    {
        var relativePath = Path.GetRelativePath(repoRoot, filePath);
        var newFileName = relativePath.Replace(Path.DirectorySeparatorChar, '-').Replace(Path.AltDirectorySeparatorChar, '-');
        var destPath = Path.Combine(destination, newFileName);
        File.Copy(filePath, destPath, true);
        copiedCount++;
    }
}

// 4. Perform Merge Operations
foreach (var entry in mergeTasks)
{
    var mergeFolderPath = entry.Key;
    var filesToMerge = entry.Value;

    if (filesToMerge.Count == 0) continue;

    var relativeMergeFolder = Path.GetRelativePath(repoRoot, mergeFolderPath);
    var outputFileName = relativeMergeFolder.Replace(Path.DirectorySeparatorChar, '-').Replace(Path.AltDirectorySeparatorChar, '-') + ".md";
    var outputPath = Path.Combine(destination, outputFileName);

    Console.WriteLine($"Merging {filesToMerge.Count} files from '{relativeMergeFolder}' into '{outputFileName}'");

    using (var writer = new StreamWriter(outputPath, false, Encoding.UTF8))
    {
        foreach (var file in filesToMerge.OrderBy(f => f))
        {
            var relativeFile = Path.GetRelativePath(repoRoot, file);
            writer.WriteLine("---");
            writer.WriteLine($"# Source: {relativeFile}");
            writer.WriteLine("---");
            writer.WriteLine();
            writer.WriteLine(File.ReadAllText(file));
            writer.WriteLine();
        }
    }
    mergedCount++;
}

Console.WriteLine("\n--- Markdown Collection Summary ---");
Console.WriteLine($"Files scanned: {processedCount}");
Console.WriteLine($"Files copied individually: {copiedCount}");
Console.WriteLine($"Folder merges performed: {mergedCount}");
Console.WriteLine($"Output directory: {destination}");

class Config
{
    public string BaseDestination { get; set; } = "";
    public string[] FoldersToMerge { get; set; } = Array.Empty<string>();
    public string[] IgnoreFolders { get; set; } = Array.Empty<string>();
    public string[] IgnoreFiles { get; set; } = Array.Empty<string>();
}
