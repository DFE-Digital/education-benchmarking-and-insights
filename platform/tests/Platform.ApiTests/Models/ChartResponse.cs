// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Platform.ApiTests.Models;

public class ChartResponse
{
    public string? Id { get; set; }
    public string? Html { get; set; }
}

public class ChartErrorResponse
{
    public string? Error { get; set; }
    public string[] Errors { get; set; } = [];
}