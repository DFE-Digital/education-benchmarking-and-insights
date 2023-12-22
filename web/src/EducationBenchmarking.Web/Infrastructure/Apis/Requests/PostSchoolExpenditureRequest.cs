using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class PostSchoolExpenditureRequest
{
    public string[]? Urns { get; set; }
    public SectionDimensions? Dimensions { get; set; }
}