using System.IO.Compression;
using AngleSharp.Io;
using Xunit.Sdk;

namespace Web.Integration.Tests;

public static class TestExtensions
{
    public static DiagnosticMessage ToDiagnosticMessage(this HttpResponseMessage message) => new(
        $"Response : {message.RequestMessage?.Method} {message.RequestMessage?.RequestUri} [{message.StatusCode}]");

    public static DiagnosticMessage ToDiagnosticMessage(this string message) => new(message);

    public static DiagnosticMessage ToDiagnosticMessage(this DocumentRequest request)
    {
        var bodyStream = new StreamReader(request.Body);
        return new DiagnosticMessage($"Submitting form : {request.Method} {request.Target} {bodyStream.ReadToEnd()}");
    }

    public static async IAsyncEnumerable<(string fileName, string content)> GetFilesFromZip(this HttpResponseMessage response)
    {
        var bytes = await response.Content.ReadAsByteArrayAsync();

        using var zipStream = new MemoryStream(bytes);
        using var archive = new ZipArchive(zipStream, ZipArchiveMode.Read);
        foreach (var entry in archive.Entries)
        {
            await using var entryStream = entry.Open();
            using var reader = new StreamReader(entryStream);
            var content = await reader.ReadToEndAsync();
            yield return (entry.Name, content);
        }
    }
}