using Web.App.Domain;

namespace Web.App.ViewModels;

public class LocalAuthorityStatisticalNeighboursViewModel(LocalAuthorityStatisticalNeighbours localAuthority)
{
    public string? Code => localAuthority.Code;
    public string? Name => localAuthority.Name;

    public string[] StatisticalNeighbours => localAuthority.StatisticalNeighbours?
        .OrderBy(n => n.Name)
        .Where(n => !string.IsNullOrWhiteSpace(n.Name))
        .Select(n => n.Name)
        .Cast<string>()
        .ToArray() ?? [];
}