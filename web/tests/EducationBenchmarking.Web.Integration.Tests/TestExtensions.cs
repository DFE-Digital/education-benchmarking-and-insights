using AngleSharp.Io;
using Xunit.Sdk;

namespace EducationBenchmarking.Web.Integration.Tests;

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
        return new DiagnosticMessage($"Submitting form : {request.Method} {request.Target}");
    }
}