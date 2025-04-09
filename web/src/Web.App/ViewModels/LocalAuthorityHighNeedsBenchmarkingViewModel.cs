using Web.App.Domain;

namespace Web.App.ViewModels;

public class LocalAuthorityHighNeedsBenchmarkingViewModel(LocalAuthorityStatisticalNeighbours localAuthority, string[] comparators, string? referrer)
{
    public string? Code => localAuthority.Code;
    public string? Name => localAuthority.Name;
    public string? Referrer => referrer;

    public string[] StatisticalNeighbours => localAuthority.StatisticalNeighbours?
        .OrderBy(n => n.Position)
        .ThenBy(n => n.Name)
        .Where(n => !string.IsNullOrWhiteSpace(n.Name))
        .Select(n => n.Name)
        .Cast<string>()
        .ToArray() ?? [];

    public string[] Comparators => comparators
        .Where(c => c != Code)
        .Distinct()
        .ToArray();
}