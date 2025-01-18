using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global

namespace Platform.Api.Establishment.Features.Trusts.Models;

[ExcludeFromCodeCoverage]
public record TrustComparator
{
    public string? CompanyNumber { get; set; }
    public double? TotalPupils { get; set; }
    public double? SchoolsInTrust { get; set; }
    public double? TotalIncome { get; set; }
    public string[]? PhasesCovered { get; set; }
    public DateTime? OpenDate { get; set; }
    public double? PercentFreeSchoolMeals { get; set; }
    public double? PercentSpecialEducationNeeds { get; set; }
    public double? TotalInternalFloorArea { get; set; }
}