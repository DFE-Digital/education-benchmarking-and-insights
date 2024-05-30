using System;
using System.Diagnostics.CodeAnalysis;
using Dapper.Contrib.Extensions;

namespace Platform.Api.Establishment.Schools;

[ExcludeFromCodeCoverage]
[Table("School")]
public record School
{
    [ExplicitKey] public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? FinanceType { get; set; }
    public string? OverallPhase { get; set; }
    public string? SchoolType { get; set; }
    public bool HasSixthForm { get; set; }
    public bool HasNursery { get; set; }
    public bool IsPFISchool { get; set; }
    public DateTime? OfstedDate { get; set; }
    public string? OfstedDescription { get; set; }
    public string? Telephone { get; set; }
    public string? Website { get; set; }
    public string? ContactEmail { get; set; }
    public string? HeadteacherName { get; set; }
    public string? HeadteacherEmail { get; set; }
    public string? TrustCompanyNumber { get; set; }
    public string? TrustName { get; set; }
    public string? FederationLeadURN { get; set; }
    public string? FederationLeadName { get; set; }
    public string? LACode { get; set; }
    public string? LAName { get; set; }
    public string? LondonWeighting { get; set; }
}