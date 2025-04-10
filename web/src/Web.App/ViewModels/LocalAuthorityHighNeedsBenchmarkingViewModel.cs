using Web.App.Domain;

namespace Web.App.ViewModels;

public class LocalAuthorityHighNeedsBenchmarkingViewModel(LocalAuthority localAuthority, string[] comparators)
{
    public string? Code => localAuthority.Code;
    public string? Name => localAuthority.Name;

    public string[] Comparators => comparators
        .Where(c => c != Code)
        .Distinct()
        .ToArray();
}