using Web.App.Domain;
using Web.App.Domain.Content;
using Web.App.Extensions;

namespace Web.App.ViewModels;

public class SchoolFinancialBenchmarkingInsightsSummaryUnavailableViewModel(School school, FinanceYears years, SchoolFinancialBenchmarkingInsightsSummaryUnavailableViewModel.UnavailableReason? reason)
{
    public enum UnavailableReason
    {
        NonLeadFederatedSchool,
        MissingExpenditure,
        PartYear
    }

    public string? Commentary => reason switch
    {
        UnavailableReason.NonLeadFederatedSchool => "this is a non lead school in a federation",
        UnavailableReason.MissingExpenditure => "expenditure data is not available for this school",
        UnavailableReason.PartYear => "this school does not have data for the entire period",
        _ => null
    };

    public string? Name => school.SchoolName;
    public string? Urn => school.URN;
    public bool IsPartOfTrust => school.IsPartOfTrust;
    public string Years => IsPartOfTrust ? years.Aar.ToFinanceYear() : years.Cfr.ToFinanceYear();
}