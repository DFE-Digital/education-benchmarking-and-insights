namespace Platform.Api.Insight.Features.Ratings;

public record RatingsQueryParameters
{
    public string[] Urns { get; set; } = [];
}