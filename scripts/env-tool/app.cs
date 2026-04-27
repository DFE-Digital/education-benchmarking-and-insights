using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;

string GetScriptDirectory([CallerFilePath] string path = "") => Path.GetDirectoryName(path) ?? throw new InvalidOperationException("Could not resolve script directory.");
var scriptDir = GetScriptDirectory();
var ConfigFileName = Path.Combine(scriptDir, "settings.json");

if (args.Length < 2)
{
    Console.WriteLine("Usage: dotnet run scripts/env-tool/app.cs <api|tests|all> <environment>");
    return;
}

// In .NET 10 file-based apps, arguments might include the script name itself
// depending on how it is invoked. We filter out the script name if present.
var effectiveArgs = args.Where(arg => !arg.EndsWith("app.cs", StringComparison.OrdinalIgnoreCase)).ToArray();

if (effectiveArgs.Length < 2)
{
    Console.WriteLine("Usage: dotnet run scripts/env-tool/app.cs <api|tests|all> <environment>");
    return;
}

string target = effectiveArgs[0].ToLower();
string environment = effectiveArgs[1].ToLower();

string[] validTargets = { "api", "tests", "all" };
if (!validTargets.Contains(target))
{
    Console.WriteLine($"Error: Invalid target '{target}'.");
    Console.WriteLine($"Valid targets: {string.Join(", ", validTargets)}");
    return;
}

if (!File.Exists(ConfigFileName))
{
    Console.WriteLine($"Error: Configuration file '{ConfigFileName}' not found.");
    Console.WriteLine("Please copy 'settings.example.json' to 'settings.json' and populate it.");
    return;
}

var configJson = File.ReadAllText(ConfigFileName);
var config = JsonNode.Parse(configJson);

if (config == null)
{
    Console.WriteLine("Error: Failed to parse configuration.");
    return;
}

var environments = config["Environments"]?.AsObject();
var availableEnvs = environments?.Select(e => e.Key).ToList() ?? new List<string>();
var envKey = availableEnvs.FirstOrDefault(e => string.Equals(e, environment, StringComparison.OrdinalIgnoreCase));

if (envKey == null)
{
    Console.WriteLine($"Error: Environment '{environment}' not defined in {ConfigFileName}.");
    if (availableEnvs.Any())
    {
        Console.WriteLine($"Available environments: {string.Join(", ", availableEnvs)}");
    }
    return;
}

var envConfig = environments![envKey];
if (envConfig == null) return;

environment = envKey; // Use the actual case from the config for reporting

var repoRoot = Path.GetFullPath(Path.Combine(scriptDir, "..", ".."));
var apiRoot = Path.Combine(repoRoot, "platform", "src", "apis");
var testsPath = Path.Combine(repoRoot, "platform", "tests", "Platform.ApiTests", "appsettings.local.json");

Console.WriteLine($"Repo root: {repoRoot}");

if (target == "api" || target == "all")
{
    var apiTargets = config["ApiTargets"]?.AsArray();
    var apiEnv = envConfig["Api"]?.AsObject();

    if (apiTargets == null) Console.WriteLine("Warning: 'ApiTargets' not found in config.");
    if (apiEnv == null) Console.WriteLine($"Warning: 'Api' section not found for environment '{environment}'.");

    if (apiTargets != null && apiEnv != null)
    {
        Console.WriteLine("--- Updating Platform APIs ---");
        foreach (var apiTarget in apiTargets)
        {
            var targetName = apiTarget?.ToString();
            if (string.IsNullOrEmpty(targetName)) continue;
            
            var settingsPath = Path.Combine(apiRoot, targetName, "local.settings.json");

            if (File.Exists(settingsPath))
            {
                var settingsJson = File.ReadAllText(settingsPath);
                var settings = JsonNode.Parse(settingsJson);
                var values = settings?["Values"]?.AsObject();

                if (values != null)
                {
                    int updatedCount = 0;
                    foreach (var prop in apiEnv)
                    {
                        if (values.ContainsKey(prop.Key))
                        {
                            values[prop.Key] = prop.Value?.DeepClone();
                            updatedCount++;
                        }
                    }
                    File.WriteAllText(settingsPath, settings?.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));
                    Console.WriteLine($" - {targetName}: Updated {updatedCount} values.");
                }
                else
                {
                    Console.WriteLine($" - {targetName}: 'Values' section not found in local.settings.json.");
                }
            }
            else
            {
                Console.WriteLine($" - {targetName}: Not found at {settingsPath}");
            }
        }
    }
}

if (target == "tests" || target == "all")
{
    var testsEnv = envConfig["ApiTests"]?.AsObject();

    if (testsEnv == null) Console.WriteLine($"Warning: 'ApiTests' section not found for environment '{environment}'.");

    if (testsEnv != null)
    {
        Console.WriteLine("--- Updating API Tests ---");
        if (File.Exists(testsPath))
        {
            var settingsJson = File.ReadAllText(testsPath);
            var settings = JsonNode.Parse(settingsJson)?.AsObject();

            if (settings != null)
            {
                int updatedCount = 0;
                foreach (var prop in testsEnv)
                {
                    var parts = prop.Key.Split(':');
                    if (parts.Length == 2)
                    {
                        var sectionName = parts[0];
                        var propertyName = parts[1];

                        if (!settings.ContainsKey(sectionName))
                        {
                            settings[sectionName] = new JsonObject();
                        }

                        var section = settings[sectionName]?.AsObject();
                        if (section != null)
                        {
                            section[propertyName] = prop.Value?.DeepClone();
                            updatedCount++;
                        }
                    }
                }
                File.WriteAllText(testsPath, settings.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));
                Console.WriteLine($" - Updated {updatedCount} test configuration values.");
            }
        }
        else
        {
            Console.WriteLine($" - API Tests configuration not found at {testsPath}");
        }
    }
}

Console.WriteLine($"Successfully switched to {environment} settings.");

