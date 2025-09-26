using System;
using System.Net;

namespace Platform.MaintenanceTasks.Features.MonitorCommercialResources.Models;

public record CommercialResourceResult
{
    public required string Title { get; init; }
    public required string Url { get; init; }
    public bool Success { get; init; }
    public HttpStatusCode? StatusCode { get; init; }
    public string? RedirectLocation { get; init; }
    public Exception? Exception { get; init; }
}