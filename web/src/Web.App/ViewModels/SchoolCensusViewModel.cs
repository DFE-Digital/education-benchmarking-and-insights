﻿using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolCensusViewModel(
    School school,
    string? userDefinedSetId = null,
    string? customDataId = null,
    Census? census = null,
    SchoolComparatorSet? defaultComparatorSet = null)
{
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;
    public bool IsPartOfTrust => school.IsPartOfTrust;
    public string? UserDefinedSetId => userDefinedSetId;
    public string? CustomDataId => customDataId;
    public decimal? TotalPupils => census?.TotalPupils;
    public bool HasDefaultComparatorSet => defaultComparatorSet != null
                                           && defaultComparatorSet.Pupil.Any(p => !string.IsNullOrWhiteSpace(p));
}