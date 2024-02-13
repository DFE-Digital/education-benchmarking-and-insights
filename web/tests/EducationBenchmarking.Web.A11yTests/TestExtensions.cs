using Microsoft.Playwright;
using Xunit.Sdk;

namespace EducationBenchmarking.Web.A11yTests;

public static class TestExtensions
{
    public static DiagnosticMessage ToDiagnosticMessage(this HttpResponseMessage message)
    {
        return new DiagnosticMessage(
            $"{message.RequestMessage?.Method} {message.RequestMessage?.RequestUri} [{message.StatusCode}]");
    }
    
    public static DiagnosticMessage ToDiagnosticMessage(this IResponse message)
    {
        return new DiagnosticMessage($"{message.Request.Method} {message.Url} [{message.Status}]");
    }
    
    public static DiagnosticMessage ToDiagnosticMessage(this string message)
    {
        return new DiagnosticMessage(message);
    }
}