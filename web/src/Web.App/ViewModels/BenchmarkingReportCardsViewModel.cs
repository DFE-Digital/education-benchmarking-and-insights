using Web.App.Domain;
using Web.App.Extensions;
namespace Web.App.ViewModels;

public class BenchmarkingReportCardsViewModel(School school, FinanceYears years, SchoolBalance? balance) : ISchoolKeyInformationViewModel
{
    public string? Name => school.SchoolName;
    public string? Urn => school.URN;
    public bool IsPartOfTrust => school.IsPartOfTrust;
    public string Years => IsPartOfTrust ? years.Aar.ToFinanceYear() : years.Cfr.ToFinanceYear();
    public string? OverallPhase => school.OverallPhase;
    public string? OfstedRating => school.OfstedDescription;
    public decimal? InYearBalance => balance?.InYearBalance;
    public decimal? RevenueReserve => balance?.RevenueReserve;
}