namespace EducationBenchmarking.Platform.Api.Insight.Models;

public class SectionDimensions
{
    public string TotalExpenditure { get; set; } = Constants.DimensionActual;
    public string TeachingSupportStaff { get; set; } = Constants.DimensionActual;
    public string NonEducationalSupportStaff { get; set; } = Constants.DimensionActual;
    public string EducationalSupplies { get; set; } = Constants.DimensionActual;
    public string EducationalIct { get; set; } = Constants.DimensionActual;
    public string PremisesStaffServices { get; set; } = Constants.DimensionActual;
    public string Utilities { get; set; } = Constants.DimensionActual;
    public string AdministrativeSupplies { get; set; } = Constants.DimensionActual;
    public string CateringStaffServices { get; set; } = Constants.DimensionActual;
    public string OtherCosts { get; set; } = Constants.DimensionActual;
}