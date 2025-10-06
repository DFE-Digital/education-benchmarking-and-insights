using Web.App.Domain;

namespace Web.App.ViewModels;

public class TrustFinancialBenchmarkingInsightsSummaryViewModel(
    Trust trust,
    TrustBalance? balance) : ITrustKeyInformationViewModel
{
    public string? Name => trust.TrustName;
    public string? CompanyNumber => trust.CompanyNumber;

    public IEnumerable<TrustSchool> Schools => trust.Schools;

    public decimal? InYearBalance => balance?.InYearBalance;
    public decimal? RevenueReserve => balance?.RevenueReserve;
    public int NumberSchools => Schools.Count();
}