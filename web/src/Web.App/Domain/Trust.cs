using System.Diagnostics.CodeAnalysis;

namespace Web.App.Domain;

[ExcludeFromCodeCoverage]
public record Trust
{
    public string? CompanyNumber { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Telephone { get; set; }
    public string? LocalAuthority { get; set; }
    public string? Website { get; set; }
    public string? Uid { get; set; }
}