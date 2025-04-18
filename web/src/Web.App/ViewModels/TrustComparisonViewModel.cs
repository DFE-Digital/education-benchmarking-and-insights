﻿using Web.App.Domain;

namespace Web.App.ViewModels;

public class TrustComparisonViewModel(Trust trust)
{
    public string? CompanyNumber => trust.CompanyNumber;
    public string? Name => trust.TrustName;
    public int NumberOfSchools => trust.Schools.Length;
    public string[] Phases => trust.Schools
        .GroupBy(x => x.OverallPhase)
        .OrderByDescending(x => x.Count())
        .Select(x => x.Key)
        .OfType<string>()
        .ToArray();
    public Dictionary<string, string> CostCodeMap => new CostCodes(true).SubCategoryToCostCodeMap;
}