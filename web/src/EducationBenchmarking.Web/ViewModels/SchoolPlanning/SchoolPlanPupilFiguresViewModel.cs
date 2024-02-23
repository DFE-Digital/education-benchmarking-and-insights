using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.ViewModels.SchoolPlanning;

public class SchoolPlanPupilFiguresViewModel(School school, FinancialPlan plan, PostPupilFiguresViewModel? form = null)
    : SchoolPlanViewModel(school, plan)
{
    public new string? PupilsYear7 => form?.PupilsYear8 ?? base.PupilsYear7;
    public new string? PupilsYear8 => form?.PupilsYear8 ?? base.PupilsYear8;
    public new string? PupilsYear9 => form?.PupilsYear9 ?? base.PupilsYear9;
    public new string? PupilsYear10 => form?.PupilsYear10 ?? base.PupilsYear10;
    public new string? PupilsYear11 => form?.PupilsYear11 ?? base.PupilsYear11;
    public new decimal? PupilsYear12 => form?.PupilsYear12 ?? base.PupilsYear12;
    public new decimal? PupilsYear13 => form?.PupilsYear13 ?? base.PupilsYear13;
}