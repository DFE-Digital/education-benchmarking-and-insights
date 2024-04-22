namespace Platform.Api.Establishment.Features.Schools;

public record SchoolQueryParameters
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
};