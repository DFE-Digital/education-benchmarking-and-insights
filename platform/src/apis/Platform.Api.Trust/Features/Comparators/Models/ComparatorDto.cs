﻿using System;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global

namespace Platform.Api.Trust.Features.Comparators.Models;

[ExcludeFromCodeCoverage]
public record ComparatorDto
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