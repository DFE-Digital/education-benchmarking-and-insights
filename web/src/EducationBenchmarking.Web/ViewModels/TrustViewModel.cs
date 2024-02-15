using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.ViewModels;

public class TrustViewModel(Trust trust)
{
    public string? Name => trust.Name;
}