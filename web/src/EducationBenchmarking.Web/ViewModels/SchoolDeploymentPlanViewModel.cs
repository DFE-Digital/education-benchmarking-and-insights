using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.Extensions;

namespace EducationBenchmarking.Web.ViewModels;

public class SchoolDeploymentPlanViewModel(School school, FinancialPlan plan)
{
    public string? Name => school.Name;
    public bool IsPrimary => school.IsPrimary;
    public int Year => plan.Year;
    public string? Urn => plan.Urn;
    public decimal TotalIncome => plan.TotalIncome ?? 0;
    public decimal TotalExpenditure => plan.TotalExpenditure ?? 0;
    public decimal TotalTeacherCosts => plan.TotalTeacherCosts ?? 0;
    public decimal EducationSupportStaffCosts => plan.EducationSupportStaffCosts ?? 0;
    public decimal TotalNumberOfTeachersFte => plan.TotalNumberOfTeachersFte ?? 0;
    public int TimetablePeriods => plan.TimetablePeriods.ToInt() ?? 0;
    public decimal PupilsNursery => plan.PupilsNursery ?? 0;
    public int PupilsReception => plan.PupilsReception.ToInt() ?? 0;
    public int PupilsYear1 => plan.PupilsYear1.ToInt() ?? 0;
    public int PupilsYear2 => plan.PupilsYear2.ToInt() ?? 0;
    public int PupilsYear3 => plan.PupilsYear3.ToInt() ?? 0;
    public int PupilsYear4 => plan.PupilsYear4.ToInt() ?? 0;
    public int PupilsYear5 => plan.PupilsYear5.ToInt() ?? 0;
    public int PupilsYear6 => plan.PupilsYear6.ToInt() ?? 0;
    public int PupilsMixedReceptionYear1 => plan.PupilsMixedReceptionYear1.ToInt() ?? 0;
    public int PupilsMixedYear1Year2 => plan.PupilsMixedYear1Year2.ToInt() ?? 0;
    public int PupilsMixedYear2Year3 => plan.PupilsMixedYear2Year3.ToInt() ?? 0;
    public int PupilsMixedYear3Year4 => plan.PupilsMixedYear3Year4.ToInt() ?? 0;
    public int PupilsMixedYear4Year5 => plan.PupilsMixedYear4Year5.ToInt() ?? 0;
    public int PupilsMixedYear5Year6 => plan.PupilsMixedYear5Year6.ToInt() ?? 0;
    public int PupilsYear7 => plan.PupilsYear7.ToInt() ?? 0;
    public int PupilsYear8 => plan.PupilsYear8.ToInt() ?? 0;
    public int PupilsYear9 => plan.PupilsYear9.ToInt() ?? 0;
    public int PupilsYear10 => plan.PupilsYear10.ToInt() ?? 0;
    public int PupilsYear11 => plan.PupilsYear11.ToInt() ?? 0;
    public decimal PupilsYear12 => plan.PupilsYear12 ?? 0;
    public decimal PupilsYear13 => plan.PupilsYear13 ?? 0;
    public int TeachersNursery => plan.TeachersNursery.ToInt() ?? 0;
    public int TeachersMixedReceptionYear1 => plan.TeachersMixedReceptionYear1.ToInt() ?? 0;
    public int TeachersMixedYear1Year2 => plan.TeachersMixedYear1Year2.ToInt() ?? 0;
    public int TeachersMixedYear2Year3 => plan.TeachersMixedYear2Year3.ToInt() ?? 0;
    public int TeachersMixedYear3Year4 => plan.TeachersMixedYear3Year4.ToInt() ?? 0;
    public int TeachersMixedYear4Year5 => plan.TeachersMixedYear4Year5.ToInt() ?? 0;
    public int TeachersMixedYear5Year6 => plan.TeachersMixedYear5Year6.ToInt() ?? 0;
    public int TeachersReception => plan.TeachersReception.ToInt() ?? 0;
    public int TeachersYear1 => plan.TeachersYear1.ToInt() ?? 0;
    public int TeachersYear2 => plan.TeachersYear2.ToInt() ?? 0;
    public int TeachersYear3 => plan.TeachersYear3.ToInt() ?? 0;
    public int TeachersYear4 => plan.TeachersYear4.ToInt() ?? 0;
    public int TeachersYear5 => plan.TeachersYear5.ToInt() ?? 0;
    public int TeachersYear6 => plan.TeachersYear6.ToInt() ?? 0;
    public int TeachersYear7 => plan.TeachersYear7.ToInt() ?? 0;
    public int TeachersYear8 => plan.TeachersYear8.ToInt() ?? 0;
    public int TeachersYear9 => plan.TeachersYear9.ToInt() ?? 0;
    public int TeachersYear10 => plan.TeachersYear10.ToInt() ?? 0;
    public int TeachersYear11 => plan.TeachersYear11.ToInt() ?? 0;
    public int TeachersYear12 => plan.TeachersYear12.ToInt() ?? 0;
    public int TeachersYear13 => plan.TeachersYear13.ToInt() ?? 0;
    public decimal AssistantsNursery => plan.AssistantsNursery ?? 0;
    public decimal AssistantsMixedReceptionYear1 => plan.AssistantsMixedReceptionYear1 ?? 0;
    public decimal AssistantsMixedYear1Year2 => plan.AssistantsMixedYear1Year2 ?? 0;
    public decimal AssistantsMixedYear2Year3 => plan.AssistantsMixedYear2Year3 ?? 0;
    public decimal AssistantsMixedYear3Year4 => plan.AssistantsMixedYear3Year4 ?? 0;
    public decimal AssistantsMixedYear4Year5 => plan.AssistantsMixedYear4Year5 ?? 0;
    public decimal AssistantsMixedYear5Year6 => plan.AssistantsMixedYear5Year6 ?? 0;
    public decimal AssistantsReception => plan.AssistantsReception ?? 0;
    public decimal AssistantsYear1 => plan.AssistantsYear1 ?? 0;
    public decimal AssistantsYear2 => plan.AssistantsYear2 ?? 0;
    public decimal AssistantsYear3 => plan.AssistantsYear3 ?? 0;
    public decimal AssistantsYear4 => plan.AssistantsYear4 ?? 0;
    public decimal AssistantsYear5 => plan.AssistantsYear5 ?? 0;
    public decimal AssistantsYear6 => plan.AssistantsYear6 ?? 0;

    public decimal TotalPupils => PupilsNursery + PupilsReception + PupilsYear1 + PupilsYear2 + PupilsYear3 +
                                    PupilsYear4 + PupilsYear5 + PupilsYear6 + PupilsMixedReceptionYear1 +
                                    PupilsMixedYear1Year2 + PupilsMixedYear2Year3 + PupilsMixedYear3Year4 +
                                    PupilsMixedYear4Year5 + PupilsMixedYear5Year6 + PupilsYear7 + PupilsYear8 +
                                    PupilsYear9 + PupilsYear10 + PupilsYear11 + PupilsYear12 + PupilsYear13;

    public decimal TeachingPeriods => TeachersNursery + TeachersReception + TeachersYear1 + TeachersYear2 +
                                      TeachersYear3 + TeachersYear4 + TeachersYear5 + TeachersYear6 +
                                      TeachersMixedReceptionYear1 + TeachersMixedYear1Year2 + TeachersMixedYear2Year3 +
                                      TeachersMixedYear3Year4 + TeachersMixedYear4Year5 + TeachersMixedYear5Year6 +
                                      TeachersYear7 + TeachersYear8 + TeachersYear9 + TeachersYear10 + TeachersYear11 +
                                      TeachersYear12 + TeachersYear13;

    public decimal TotalTeachingAssistants => AssistantsNursery + AssistantsReception + AssistantsYear1 +
                                              AssistantsYear2 + AssistantsYear3 + AssistantsYear4 + AssistantsYear5 +
                                              AssistantsYear6 + AssistantsMixedReceptionYear1 +
                                              AssistantsMixedYear1Year2 + AssistantsMixedYear2Year3 +
                                              AssistantsMixedYear3Year4 + AssistantsMixedYear4Year5 +
                                              AssistantsMixedYear5Year6;

    public decimal OtherPeriods => plan.OtherTeachingPeriods
        .Sum(x => x.PeriodsPerTimetable.ToInt() ?? 0);

    public decimal TotalTeachingPeriods => TeachingPeriods + OtherPeriods;

    public decimal PupilTeacherRatio => TotalPupils / TotalNumberOfTeachersFte;

    public decimal AverageClassSize => TotalPupils * TimetablePeriods / TotalTeachingPeriods;

    public Rating AverageClassSizeRating =>
        RatingCalculations.AverageClassSize(AverageClassSize);

    public decimal AverageTeacherLoad => TotalTeachingPeriods / TotalNumberOfTeachersFte;

    public decimal TeacherContactRatio => TotalTeachingPeriods / (TimetablePeriods * TotalNumberOfTeachersFte);
    public Rating TeacherContactRatioRating =>
        RatingCalculations.ContactRatio(TeacherContactRatio);

    public decimal IncomePerPupil => TotalIncome / TotalPupils;

    public decimal TeacherCostPercentageExpenditure => TotalTeacherCosts / TotalExpenditure * 100;

    public decimal TeacherCostPercentageIncome => TotalTeacherCosts / TotalIncome * 100;

    public decimal InYearBalance => TotalIncome - TotalExpenditure;
    public Rating InYearBalanceRating =>
        RatingCalculations.InYearBalancePercentIncome(InYearBalance / TotalIncome * 100);

    public decimal CostPerLesson => TotalTeachingPeriods / TotalTeacherCosts;

    public decimal AverageTeacherCost => TotalTeacherCosts / TotalNumberOfTeachersFte;

    public decimal AverageTeachingAssistantCost => EducationSupportStaffCosts / TotalTeachingAssistants;
}