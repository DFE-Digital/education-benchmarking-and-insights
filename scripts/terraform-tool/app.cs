using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

string GetScriptDirectory([CallerFilePath] string path = "") => Path.GetDirectoryName(path) ?? throw new InvalidOperationException("Could not resolve script directory.");
var scriptDir = GetScriptDirectory();
var ConfigFileName = Path.Combine(scriptDir, "settings.json");

if (!File.Exists(ConfigFileName))
{
    Console.WriteLine($"Error: Configuration file '{ConfigFileName}' not found.");
    Console.WriteLine("Please copy 'settings.example.json' to 'settings.json' and populate it.");
    Console.WriteLine("Usage: dotnet run scripts/terraform-tool/app.cs");
    return;
}

var configJson = File.ReadAllText(ConfigFileName);
Config config;
using (var doc = JsonDocument.Parse(configJson))
{
    var root = doc.RootElement;
    config = new Config
    {
        EnableFmt = !root.TryGetProperty("EnableFmt", out var fmt) || fmt.GetBoolean(),
        EnableValidate = !root.TryGetProperty("EnableValidate", out var valid) || valid.GetBoolean(),
        EnableDocs = !root.TryGetProperty("EnableDocs", out var docs) || docs.GetBoolean(),
        EnableTFLint = root.TryGetProperty("EnableTFLint", out var tflint) && tflint.GetBoolean(),
        EnableCheckov = root.TryGetProperty("EnableCheckov", out var checkov) && checkov.GetBoolean()
    };
}

var repoRoot = Path.GetFullPath(Path.Combine(scriptDir, "..", ".."));

Console.WriteLine("Scanning for terraform entry points...");
var entryPoints = Directory.GetDirectories(repoRoot, "terraform", SearchOption.AllDirectories)
    .Where(d => {
        var parts = d.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        return !parts.Any(p => p.StartsWith(".") || p == "node_modules" || p == "bin" || p == "obj");
    })
    .Distinct()
    .ToList();

Console.WriteLine("Scanning for all modules (directories with .tf files)...");
var allModules = Directory.GetFiles(repoRoot, "*.tf", SearchOption.AllDirectories)
    .Select(Path.GetDirectoryName)
    .Where(d => d != null)
    .Select(d => d!)
    .Where(d => {
        var parts = d.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        return !parts.Any(p => p.StartsWith(".") || p == "node_modules" || p == "bin" || p == "obj");
    })
    .Distinct()
    .ToList();

Console.WriteLine($"Found {entryPoints.Count} entry points and {allModules.Count} total modules.");

// Pre-run tool check
var requiredTools = new List<string>();
if (config.EnableFmt || config.EnableValidate) requiredTools.Add("terraform");
if (config.EnableDocs) requiredTools.Add("terraform-docs");
if (config.EnableTFLint) requiredTools.Add("tflint");
if (config.EnableCheckov) requiredTools.Add("docker");

var missingTools = new List<string>();
foreach (var tool in requiredTools)
{
    if (!await ToolExists(tool)) missingTools.Add(tool);
}

if (missingTools.Any())
{
    Console.WriteLine("\nError: Missing required CLI tools. Please install the following and ensure they are in your PATH:");
    foreach (var tool in missingTools) Console.WriteLine($" - {tool}");
    return;
}

var results = new List<ModuleResult>();

// Phase 1: Process Entry Points (Fmt, Validate, Lint concurrently)
if (config.EnableFmt || config.EnableValidate || config.EnableTFLint)
{
    Console.WriteLine("\nProcessing entry points...");
    var entryTasks = entryPoints.Select(async path =>
    {
        var moduleName = Path.GetRelativePath(repoRoot, path);
        var result = new ModuleResult { ModuleName = moduleName, IsEntryPoint = true };

        if (config.EnableFmt) result.Fmt = await RunCommand("terraform", "fmt -recursive", path);

        if (config.EnableValidate)
        {
            // Init is required for Validate
            var initSuccess = await RunCommand("terraform", "init -backend=false -upgrade", path);
            result.Validate = initSuccess 
                ? await RunCommand("terraform", "validate", path)
                : false;
        }
        
        if (config.EnableTFLint) result.TFLint = await RunCommand("tflint", "", path);

        lock (results) { results.Add(result); }
    });

    await Task.WhenAll(entryTasks);
}

// Phase 2: Process All Modules (Docs concurrently)
if (config.EnableDocs)
{
    Console.WriteLine("Generating documentation for all modules...");
    var docsTasks = allModules.Select(async path =>
    {
        var moduleName = Path.GetRelativePath(repoRoot, path);
        var success = await RunCommand("terraform-docs", "markdown table --output-file README.md .", path);
        
        lock (results)
        {
            var existing = results.FirstOrDefault(r => r.ModuleName == moduleName);
            if (existing != null)
            {
                existing.Docs = success;
            }
            else
            {
                results.Add(new ModuleResult { ModuleName = moduleName, Docs = success, IsEntryPoint = false });
            }
        }
    });

    await Task.WhenAll(docsTasks);
}

// Phase 3: Run Checkov (Sequentially)
if (config.EnableCheckov)
{
    Console.WriteLine("Running Checkov for entry points...");
    foreach(var path in entryPoints)
    {
        var moduleName = Path.GetRelativePath(repoRoot, path);
        var absolutePath = Path.GetFullPath(path);
        Console.WriteLine($" - Scanning {moduleName}...");
        var dockerArgs = $"run --rm -v \"{absolutePath}:/tf\" bridgecrew/checkov:latest -d /tf --output cli";
        var success = await RunCommand("docker", dockerArgs, path);

         lock (results)
         {
             var existing = results.FirstOrDefault(r => r.ModuleName == moduleName);
             if (existing != null)
             {
                 existing.Checkov = success;
             }
             else
             {
                 results.Add(new ModuleResult { ModuleName = moduleName, Checkov = success, IsEntryPoint = false });
             }
         }
    }
}


Console.WriteLine("\n--- Terraform Process Summary ---");
Console.Write($"{"Module",-60}");
if (config.EnableFmt) Console.Write($" | {"Fmt",-5}");
if (config.EnableValidate) Console.Write($" | {"Valid",-5}");
if (config.EnableDocs) Console.Write($" | {"Docs",-5}");
if (config.EnableTFLint) Console.Write($" | {"Lint",-5}");
if (config.EnableCheckov) Console.Write($" | {"Check",-5}");
Console.WriteLine();
Console.WriteLine(new string('-', 120));

foreach (var res in results.OrderBy(r => r.ModuleName))
{
    Console.Write($"{res.ModuleName,-60}");
    if (config.EnableFmt) Console.Write($" | {(res.IsEntryPoint ? Status(res.Fmt) : "-"),-5}");
    if (config.EnableValidate) Console.Write($" | {(res.IsEntryPoint ? Status(res.Validate) : "-"),-5}");
    if (config.EnableDocs) Console.Write($" | {Status(res.Docs),-5}");
    if (config.EnableTFLint) Console.Write($" | {(res.IsEntryPoint ? Status(res.TFLint) : "-"),-5}");
    if (config.EnableCheckov) Console.Write($" | {(res.IsEntryPoint ? Status(res.Checkov) : "-"),-5}");
    Console.WriteLine();
}

string Status(bool? success) => success == null ? "-" : (success.Value ? "PASS" : "FAIL");

async Task<bool> ToolExists(string command)
{
    try
    {
        var checkCmd = OperatingSystem.IsWindows() ? "where" : "which";
        var startInfo = new ProcessStartInfo
        {
            FileName = checkCmd,
            Arguments = command,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = new Process { StartInfo = startInfo };
        process.Start();
        await process.WaitForExitAsync();
        return process.ExitCode == 0;
    }
    catch { return false; }
}

async Task<bool> RunCommand(string command, string args, string workingDir)
{
    try
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = command,
            Arguments = args,
            WorkingDirectory = workingDir,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        
        if (command == "docker")
        {
            startInfo.EnvironmentVariables["MSYS_NO_PATHCONV"] = "1";
        }

        using var process = new Process { StartInfo = startInfo };
        process.Start();
        
        var consumeOut = process.StandardOutput.ReadToEndAsync();
        var consumeErr = process.StandardError.ReadToEndAsync();
        
        await process.WaitForExitAsync();
        await Task.WhenAll(consumeOut, consumeErr);
        
        return process.ExitCode == 0;
    }
    catch { return false; }
}

class Config
{
    public bool EnableFmt { get; set; } = true;
    public bool EnableValidate { get; set; } = true;
    public bool EnableDocs { get; set; } = true;
    public bool EnableTFLint { get; set; }
    public bool EnableCheckov { get; set; }
}

class ModuleResult
{
    public string ModuleName { get; set; } = "";
    public bool IsEntryPoint { get; set; }
    public bool? Fmt { get; set; }
    public bool? Validate { get; set; }
    public bool? Docs { get; set; }
    public bool? TFLint { get; set; }
    public bool? Checkov { get; set; }
}
