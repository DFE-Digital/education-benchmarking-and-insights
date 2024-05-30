using System;
using System.Diagnostics.CodeAnalysis;
using Dapper.Contrib.Extensions;

namespace Platform.Api.Establishment.Trusts;

[ExcludeFromCodeCoverage]
[Table("Trust")]
public record Trust
{
    [ExplicitKey] public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
    public string? UID { get; set; }
    public string? CFOName { get; set; }
    public string? CFOEmail { get; set; }
    public DateTime? OpenDate { get; set; }
}