namespace Platform.MaintenanceTasks.Features.MonitorCommercialResources.Models;

public record CommercialResource
{
    public required string Title { get; init; }
    public required string Url { get; init; }
}