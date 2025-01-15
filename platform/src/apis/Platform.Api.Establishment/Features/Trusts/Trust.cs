using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

namespace Platform.Api.Establishment.Features.Trusts;

[ExcludeFromCodeCoverage]
public record Trust
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
    public string? UID { get; set; }

    public string? CFOName { get; set; }
    public string? CFOEmail { get; set; }
    public DateTime? OpenDate { get; set; }

    public IEnumerable<TrustSchool>? Schools { get; set; }
}

[ExcludeFromCodeCoverage]
public record TrustSchool
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? OverallPhase { get; set; }
}