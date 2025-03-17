using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.NonFinancial.Shared;

[ExcludeFromCodeCoverage]
public record YearsModel
{
    public int StartYear { get; set; }
    public int EndYear { get; set; }
}