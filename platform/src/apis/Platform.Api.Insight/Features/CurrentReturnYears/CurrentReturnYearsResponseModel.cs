namespace Platform.Api.Insight.Features.CurrentReturnYears;

public record CurrentReturnYearsResponseModel
{
    public int? Aar { get; set; }
    public int? Cfr { get; set; }
};