using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.ViewModels;

public class TrustViewModel
{
    private readonly Trust _trust;
    private readonly Finances _finances;
    public TrustViewModel(Trust trust, Finances finances)
    {
        _trust = trust;
        _finances = finances;
    }

    public string CompanyNumber => _trust.CompanyNumber;
    public int LastFinancialYear { get; set; }
    public string Name => _trust.Name;
}