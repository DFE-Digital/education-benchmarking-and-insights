using AngleSharp.Io;
using Web.App.Extensions;
using Xunit.Sdk;

namespace Web.Integration.Tests;

public static class TestExtensions
{
    public static DiagnosticMessage ToDiagnosticMessage(this HttpResponseMessage message)
    {
        return new DiagnosticMessage(
            $"Response : {message.RequestMessage?.Method} {message.RequestMessage?.RequestUri} [{message.StatusCode}]");
    }

    public static DiagnosticMessage ToDiagnosticMessage(this string message)
    {
        return new DiagnosticMessage(message);
    }

    public static DiagnosticMessage ToDiagnosticMessage(this DocumentRequest request)
    {
        var bodyStream = new StreamReader(request.Body);
        return new DiagnosticMessage($"Submitting form : {request.Method} {request.Target} {bodyStream.ReadToEnd()}");
    }
}