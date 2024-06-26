﻿using System.ComponentModel.DataAnnotations;
using Web.App.Attributes;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Web.App.ViewModels;

public interface INonFinancialDataCustomDataViewModel : ICustomDataViewModel
{
    decimal? NumberOfPupilsFte { get; }
    decimal? FreeSchoolMealPercent { get; }
    decimal? SpecialEducationalNeedsPercent { get; }
    decimal? FloorArea { get; }
}

public record NonFinancialDataCustomDataViewModel : INonFinancialDataCustomDataViewModel
{
    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.NumberOfPupilsFte)]
    public decimal? NumberOfPupilsFte { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.FreeSchoolMealPercent)]
    public decimal? FreeSchoolMealPercent { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.SpecialEducationalNeedsPercent)]
    public decimal? SpecialEducationalNeedsPercent { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.FloorArea)]
    public decimal? FloorArea { get; init; }
}