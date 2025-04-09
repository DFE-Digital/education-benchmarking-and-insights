using Web.App.Domain;

namespace Web.App.ViewModels;

public class LocalAuthorityHighNeedsBenchmarkingViewModel(LocalAuthority localAuthority, string[] comparators, string? referrer)
{
    public string? Code => localAuthority.Code;
    public string? Name => localAuthority.Name;
    public string? Referrer => referrer;

    public string[] Comparators => comparators
        .Where(c => c != Code)
        .Distinct()
        .ToArray();
}