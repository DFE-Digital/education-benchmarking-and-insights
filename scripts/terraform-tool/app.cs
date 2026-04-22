using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

const string ConfigFileName = "settings.json";

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
        Modules = root.TryGetProperty("Modules", out var modules) 
            ? modules.EnumerateArray().Select(x => x.GetString() ?? "").Where(s => !string.IsNullOrEmpty(s)).ToArray() 
            : Array.Empty<string>(),
        EnableTFLint = root.TryGetProperty("EnableTFLint", out var tflint) && tflint.GetBoolean(),
        EnableCheckov = root.TryGetProperty("EnableCheckov", out var checkov) && checkov.GetBoolean()
    };
}

var repoRoot = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", ".."));
var modulesToProcess = config.Modules.Select(m => Path.Combine(repoRoot, m)).ToList();

if (modulesToProcess.Count == 0)
{
    Console.WriteLine("Scanning for terraform modules...");
    modulesToProcess = Directory.GetDirectories(repoRoot, "terraform", SearchOption.AllDirectories)
        .Where(d => !d.Contains(".terraform") && !d.Contains("node_modules") && !d.Contains(".git"))
        .ToList();
}

Console.WriteLine($"Found {modulesToProcess.Count} modules to process.");

// Pre-run tool check
var requiredTools = new List<string> { "terraform", "terraform-docs" };
if (config.EnableTFLint) requiredTools.Add("tflint");
if (config.EnableCheckov) requiredTools.Add("checkov");

var missingTools = new List<string>();
foreach (var tool in requiredTools)
{
    if (!await ToolExists(tool))
    {
        missingTools.Add(tool);
    }
}

if (missingTools.Any())
{
    Console.WriteLine("\nError: Missing required CLI tools. Please install the following and ensure they are in your PATH:");
    foreach (var tool in missingTools)
    {
        Console.WriteLine($" - {tool}");
    }
    return;
}

var results = new List<ModuleResult>();
var tasks = modulesToProcess.Select(async modulePath =>
{
    var moduleName = Path.GetRelativePath(repoRoot, modulePath);
    var result = new ModuleResult { ModuleName = moduleName };
    
    result.Fmt = await RunCommand("terraform", "fmt -recursive", modulePath);
    result.Init = await RunCommand("terraform", "init -backend=false -upgrade", modulePath);
    result.Validate = await RunCommand("terraform", "validate", modulePath);
    result.Docs = await RunCommand("terraform-docs", "markdown table --output-file README.md .", modulePath);

    if (config.EnableTFLint)
        result.TFLint = await RunCommand("tflint", "", modulePath);
    
    if (config.EnableCheckov)
        result.Checkov = await RunCommand("checkov", "-d .", modulePath);

    lock (results) { results.Add(result); }
});

await Task.WhenAll(tasks);

Console.WriteLine("\n--- Terraform Process Summary ---");
Console.WriteLine($"{"Module",-40} | {"Fmt",-5} | {"Init",-5} | {"Valid",-5} | {"Docs",-5}{(config.EnableTFLint ? " | Lint" : "")}{(config.EnableCheckov ? " | Check" : "")}");
Console.WriteLine(new string('-', 80));

foreach (var res in results.OrderBy(r => r.ModuleName))
{
    Console.WriteLine($"{res.ModuleName,-40} | {Status(res.Fmt),-5} | {Status(res.Init),-5} | {Status(res.Validate),-5} | {Status(res.Docs),-5}{(config.EnableTFLint ? $" | {Status(res.TFLint)}" : "")}{(config.EnableCheckov ? $" | {Status(res.Checkov)}" : "")}");
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
    catch
    {
        return false;
    }
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

        using var process = new Process { StartInfo = startInfo };
        process.Start();
        await process.WaitForExitAsync();
        return process.ExitCode == 0;
    }
    catch
    {
        return false;
    }
}

class Config
{
    public string[] Modules { get; set; } = Array.Empty<string>();
    public bool EnableTFLint { get; set; }
    public bool EnableCheckov { get; set; }
}

class ModuleResult
{
    public string ModuleName { get; set; } = "";
    public bool Fmt { get; set; }
    public bool Init { get; set; }
    public bool Validate { get; set; }
    public bool Docs { get; set; }
    public bool? TFLint { get; set; }
    public bool? Checkov { get; set; }
}
