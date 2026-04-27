#:package Microsoft.Data.SqlClient@6.0.1

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Runtime.CompilerServices;
using Microsoft.Data.SqlClient;

string GetScriptDirectory([CallerFilePath] string path = "") => Path.GetDirectoryName(path) ?? throw new InvalidOperationException("Could not resolve script directory.");
var scriptDir = GetScriptDirectory();
var ConfigFileName = Path.Combine(scriptDir, "settings.json");

if (!File.Exists(ConfigFileName))
{
    Console.WriteLine($"Error: Configuration file '{ConfigFileName}' not found.");
    Console.WriteLine("Please copy 'settings.example.json' to 'settings.json' and populate it.");
    Console.WriteLine("Usage: dotnet run scripts/db-copy-tool/app.cs");
    return;
}

var configJson = File.ReadAllText(ConfigFileName);
Config config;
using (var doc = JsonDocument.Parse(configJson))
{
    var root = doc.RootElement;
    config = new Config
    {
        SourceConnectionString = GetProperty(root, "SourceConnectionString"),
        TargetConnectionString = GetProperty(root, "TargetConnectionString"),
        IgnoredTables = root.TryGetProperty("IgnoredTables", out var ignored) 
            ? ignored.EnumerateArray().Select(x => x.GetString() ?? "").ToArray() 
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

if (config == null || string.IsNullOrWhiteSpace(config.SourceConnectionString) || string.IsNullOrWhiteSpace(config.TargetConnectionString))
{
    Console.WriteLine("Error: Invalid configuration. Please check your connection strings.");
    return;
}

var ignoredTables = new HashSet<string>(config.IgnoredTables ?? Array.Empty<string>(), StringComparer.OrdinalIgnoreCase);

var copiedTables = new List<string>();
var missingInTarget = new List<string>();
var notEmptyInTarget = new List<string>();
var failedTables = new List<(string Table, string Error)>();

Console.WriteLine("Discovering tables from source...");

var sourceTables = GetSourceTables(config.SourceConnectionString);
var filteredTables = sourceTables.Where(t => 
    !ignoredTables.Contains(t.Name) && 
    !ignoredTables.Contains($"{t.Schema}.{t.Name}")).ToList();

Console.WriteLine($"Found {filteredTables.Count} tables to process (ignored {sourceTables.Count - filteredTables.Count}).");

foreach (var table in filteredTables)
{
    string fullName = $"[{table.Schema}].[{table.Name}]";
    try
    {
        if (!TableExists(config.TargetConnectionString, table.Schema, table.Name))
        {
            Console.WriteLine($"Skipping {fullName}: Table does not exist in target.");
            missingInTarget.Add(fullName);
            continue;
        }

        if (!IsTableEmpty(config.TargetConnectionString, fullName))
        {
            Console.WriteLine($"Skipping {fullName}: Target table is not empty.");
            notEmptyInTarget.Add(fullName);
            continue;
        }

        Console.Write($"Copying {fullName}...");
        CopyTableData(config.SourceConnectionString, config.TargetConnectionString, fullName);
        Console.WriteLine(" Done.");
        copiedTables.Add(fullName);
    }
    catch (Exception ex)
    {
        Console.WriteLine($" Error: {ex.Message}");
        failedTables.Add((fullName, ex.Message));
    }
}

Console.WriteLine("\n--- Copy Summary ---");
Console.WriteLine($"Successfully copied: {copiedTables.Count}");
Console.WriteLine($"Skipped (Missing in Target): {missingInTarget.Count}");
Console.WriteLine($"Skipped (Not Empty in Target): {notEmptyInTarget.Count}");
if (failedTables.Count > 0)
{
    Console.WriteLine($"Failed: {failedTables.Count}");
}

if (missingInTarget.Count > 0)
{
    Console.WriteLine("\nMissing in Target:");
    foreach (var t in missingInTarget) Console.WriteLine($" - {t}");
}

if (notEmptyInTarget.Count > 0)
{
    Console.WriteLine("\nNot Empty in Target (Skipped):");
    foreach (var t in notEmptyInTarget) Console.WriteLine($" - {t}");
}

if (failedTables.Count > 0)
{
    Console.WriteLine("\nFailed Tables:");
    foreach (var f in failedTables) Console.WriteLine($" - {f.Table}: {f.Error}");
}

List<(string Schema, string Name)> GetSourceTables(string connectionString)
{
    var tables = new List<(string Schema, string Name)>();
    using var conn = new SqlConnection(connectionString);
    conn.Open();
    using var cmd = new SqlCommand(@"
        SELECT s.name AS SchemaName, t.name AS TableName
        FROM sys.tables t
        JOIN sys.schemas s ON t.schema_id = s.schema_id
        WHERE t.is_ms_shipped = 0", conn);
    using var reader = cmd.ExecuteReader();
    while (reader.Read())
    {
        tables.Add((reader.GetString(0), reader.GetString(1)));
    }
    return tables;
}

bool TableExists(string connectionString, string schema, string name)
{
    using var conn = new SqlConnection(connectionString);
    conn.Open();
    using var cmd = new SqlCommand(@"
        SELECT 1 FROM sys.tables t
        JOIN sys.schemas s ON t.schema_id = s.schema_id
        WHERE s.name = @schema AND t.name = @name", conn);
    cmd.Parameters.AddWithValue("@schema", schema);
    cmd.Parameters.AddWithValue("@name", name);
    return cmd.ExecuteScalar() != null;
}

bool IsTableEmpty(string connectionString, string fullName)
{
    using var conn = new SqlConnection(connectionString);
    conn.Open();
    using var cmd = new SqlCommand($"SELECT TOP 1 1 FROM {fullName}", conn);
    return cmd.ExecuteScalar() == null;
}

void CopyTableData(string sourceConnStr, string targetConnStr, string fullName)
{
    using var sourceConn = new SqlConnection(sourceConnStr);
    sourceConn.Open();
    using var cmd = new SqlCommand($"SELECT * FROM {fullName}", sourceConn);
    using var reader = cmd.ExecuteReader();

    using var targetConn = new SqlConnection(targetConnStr);
    targetConn.Open();
    using var bulkCopy = new SqlBulkCopy(targetConn)
    {
        DestinationTableName = fullName,
        BulkCopyTimeout = 0,
        BatchSize = 5000
    };
    bulkCopy.WriteToServer(reader);
}

class Config
{
    public string SourceConnectionString { get; set; } = "";
    public string TargetConnectionString { get; set; } = "";
    public string[] IgnoredTables { get; set; } = Array.Empty<string>();
}
