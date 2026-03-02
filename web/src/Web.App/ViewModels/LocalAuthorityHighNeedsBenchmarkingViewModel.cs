using Web.App.Domain;
using Web.App.Domain.Content;

namespace Web.App.ViewModels;

public class LocalAuthorityHighNeedsBenchmarkingViewModel(LocalAuthority localAuthority, string[] comparators, FinanceYears years)
{
    public string? Code => localAuthority.Code;
    public string? Name => localAuthority.Name;
    public string? YearsLabel => $"({years.S251 - 1}/{years.S251})";

    public string[] Comparators => comparators
        .Where(c => c != Code)
        .Distinct()
        .ToArray();
}