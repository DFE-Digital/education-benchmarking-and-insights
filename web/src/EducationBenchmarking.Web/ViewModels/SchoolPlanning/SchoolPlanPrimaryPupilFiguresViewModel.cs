using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.ViewModels.SchoolPlanning;

public class SchoolPlanPrimaryPupilFiguresViewModel(School school, FinancialPlan plan, PostPrimaryPupilFiguresViewModel? form = null)
: SchoolPlanViewModel(school, plan)
{
    public new string? PupilsMixedReceptionYear1 => form?.PupilsMixedReceptionYear1 ?? base.PupilsMixedReceptionYear1;
    public new string? PupilsMixedYear1Year2 => form?.PupilsMixedYear1Year2 ?? base.PupilsMixedYear1Year2;
    public new string? PupilsMixedYear2Year3 => form?.PupilsMixedYear2Year3 ?? base.PupilsMixedYear2Year3;
    public new string? PupilsMixedYear3Year4 => form?.PupilsMixedYear3Year4 ?? base.PupilsMixedYear3Year4;
    public new string? PupilsMixedYear4Year5 => form?.PupilsMixedYear4Year5 ?? base.PupilsMixedYear4Year5;
    public new string? PupilsMixedYear5Year6 => form?.PupilsMixedYear5Year6 ?? base.PupilsMixedYear5Year6;
    public new string? PupilsReception => form?.PupilsReception ?? base.PupilsReception;
    public new string? PupilsYear1 => form?.PupilsYear1 ?? base.PupilsYear1;
    public new string? PupilsYear2 => form?.PupilsYear2 ?? base.PupilsYear2;
    public new string? PupilsYear3 => form?.PupilsYear3 ?? base.PupilsYear3;
    public new string? PupilsYear4 => form?.PupilsYear4 ?? base.PupilsYear4;
    public new string? PupilsYear5 => form?.PupilsYear5 ?? base.PupilsYear5;
    public new string? PupilsYear6 => form?.PupilsYear6 ?? base.PupilsYear6;
}