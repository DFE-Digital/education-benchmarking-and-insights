namespace Platform.Api.Insight.Features.Schools;

public record SchoolsQueryParameters
{
    public string[] Urns { get; set; } = [];
};