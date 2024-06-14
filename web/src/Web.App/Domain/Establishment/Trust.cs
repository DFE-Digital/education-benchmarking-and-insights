using System.Diagnostics.CodeAnalysis;

namespace Web.App.Domain;

[ExcludeFromCodeCoverage]
public record Trust
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
    public string? UID { get; set; }
    public string? CFOName { get; set; }
    public string? CFOEmail { get; set; }
    public DateTime? OpenDate { get; set; }
}