using System.ComponentModel.DataAnnotations;
using Web.App.Attributes;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Web.App.ViewModels;

public interface IWorkforceDataCustomDataViewModel : ICustomDataViewModel
{
    decimal? WorkforceFte { get; }
    decimal? TeachersFte { get; }
    decimal? SeniorLeadershipFte { get; }
}

public record WorkforceDataCustomDataViewModel : IWorkforceDataCustomDataViewModel
{
    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.WorkforceFte)]
    public decimal? WorkforceFte { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.TeachersFte)]
    [CompareDecimalValue(nameof(WorkforceFte), Operator.LessThan)]
    public decimal? TeachersFte { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.SeniorLeadershipFte)]
    [CompareDecimalValue(nameof(TeachersFte), Operator.LessThan)]
    public decimal? SeniorLeadershipFte { get; init; }
}