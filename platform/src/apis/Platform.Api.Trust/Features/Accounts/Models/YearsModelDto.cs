using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Trust.Features.Accounts.Models;

[ExcludeFromCodeCoverage]
public record YearsModelDto
{
    public int StartYear { get; set; }
    public int EndYear { get; set; }
}