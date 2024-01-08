using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.ViewModels;

public class TrustViewModel
{
    private readonly Trust _trust;
    public TrustViewModel(Trust trust)
    {
        _trust = trust;
    }
    public string Name => _trust.Name;
}