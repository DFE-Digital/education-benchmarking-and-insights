namespace EducationBenchmarking.Web.ViewModels.SchoolPlanning;

public class SchoolPlanMixedAgeClassesViewModel
{
    public bool MixedAgeReceptionYear1 { get; set; }
    public bool MixedAgeYear1Year2 { get; set; }
    public bool MixedAgeYear2Year3 { get; set; }
    public bool MixedAgeYear3Year4 { get; set; }
    public bool MixedAgeYear4Year5 { get; set; }
    public bool MixedAgeYear5Year6 { get; set; }

    public bool HasSelection => MixedAgeReceptionYear1 || MixedAgeYear1Year2 || MixedAgeYear2Year3 ||
                                MixedAgeYear3Year4 || MixedAgeYear4Year5 || MixedAgeYear5Year6;
}