using Microsoft.Playwright;
using Xunit.Sdk;

namespace Web.A11yTests;

public static class TestExtensions
{
    public static DiagnosticMessage ToDiagnosticMessage(this HttpResponseMessage message) => new(
        $"{message.RequestMessage?.Method} {message.RequestMessage?.RequestUri} [{message.StatusCode}]");

    public static DiagnosticMessage ToDiagnosticMessage(this IResponse message) => new($"{message.Request.Method} {message.Url} [{message.Status}]");

    public static DiagnosticMessage ToDiagnosticMessage(this string message) => new(message);
}