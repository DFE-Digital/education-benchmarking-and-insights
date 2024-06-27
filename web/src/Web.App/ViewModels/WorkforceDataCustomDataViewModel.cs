using System.ComponentModel.DataAnnotations;
using Web.App.Attributes;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Web.App.ViewModels;

public interface IWorkforceDataCustomDataViewModel : ICustomDataViewModel
{
    decimal? WorkforceFte { get; }
    decimal? TeachersFte { get; }
    decimal? QualifiedTeacherPercent { get; }
    decimal? SeniorLeadershipFte { get; }
    decimal? TeachingAssistantsFte { get; }
    decimal? NonClassroomSupportStaffFte { get; }
    decimal? AuxiliaryStaffFte { get; }
    decimal? WorkforceHeadcount { get; }
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
    [Display(Name = SchoolCustomDataViewModelTitles.QualifiedTeacherPercent)]
    [Range(1, 100)]
    public decimal? QualifiedTeacherPercent { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.SeniorLeadershipFte)]
    [CompareDecimalValue(nameof(TeachersFte), Operator.LessThan)]
    public decimal? SeniorLeadershipFte { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.TeachingAssistantsFte)]
    [CompareDecimalValue(nameof(WorkforceFte), Operator.LessThan)]
    public decimal? TeachingAssistantsFte { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.NonClassroomSupportStaffFte)]
    [CompareDecimalValue(nameof(WorkforceFte), Operator.LessThan)]
    public decimal? NonClassroomSupportStaffFte { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.AuxiliaryStaffFte)]
    [CompareDecimalValue(nameof(WorkforceFte), Operator.LessThan)]
    public decimal? AuxiliaryStaffFte { get; init; }

    [PositiveNumericValue]
    [Display(Name = SchoolCustomDataViewModelTitles.WorkforceHeadcount)]
    public decimal? WorkforceHeadcount { get; init; }
}