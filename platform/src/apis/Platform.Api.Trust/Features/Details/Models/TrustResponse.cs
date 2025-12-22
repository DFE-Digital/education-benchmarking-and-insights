using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Trust.Features.Details.Models;

[ExcludeFromCodeCoverage]
public record TrustResponse
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
    public string? UID { get; set; }

    public string? CFOName { get; set; }
    public string? CFOEmail { get; set; }
    public DateTime? OpenDate { get; set; }

    public IEnumerable<TrustSchoolResponse>? Schools { get; set; }
}

public record TrustSchoolResponse
{
    public static readonly string[] Fields = [nameof(URN), nameof(SchoolName), nameof(OverallPhase)];
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? OverallPhase { get; set; }
}