using Web.App.Domain;
namespace Web.App.ViewModels;

public class LocalAuthorityHighNeedsViewModel(LocalAuthority localAuthority, FinanceYears years)
{
    public string? Code => localAuthority.Code;
    public string? Name => localAuthority.Name;
    public int? Section251Year => years.S251;
    public string Section251YearRange => $"{Section251Year - 1}-{Section251Year}";
    public string ChartTitle => $"National view {Section251YearRange}";
}