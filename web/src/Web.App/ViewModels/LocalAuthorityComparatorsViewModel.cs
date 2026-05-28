using Web.App.Domain;

namespace Web.App.ViewModels;

public class LocalAuthorityComparatorsViewModel(LocalAuthorityStatisticalNeighbours localAuthority, string[] comparators, LocalAuthorityBenchmarkType type, string? referrer, IEnumerable<LocalAuthority> allLocalAuthorities)
{
    public string? Code => localAuthority.Code;
    public string? Name => localAuthority.Name;
    public LocalAuthorityBenchmarkType Type => type;
    public string? Referrer => referrer;
    public IEnumerable<LocalAuthority> LocalAuthorities => allLocalAuthorities;
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

    public RemovableItemCardViewModel[] NeighbourComparators => Comparators
        .Join(allLocalAuthorities, c => c, l => l.Code, (_, la) => la)
        .Where(la => localAuthority.StatisticalNeighbours != null && localAuthority.StatisticalNeighbours.Any(n => n.Code == la.Code))
        .Select(ToCard)
        .ToArray();

    public RemovableItemCardViewModel[] OtherComparators => Comparators
        .Join(allLocalAuthorities, c => c, l => l.Code, (_, la) => la)
        .Where(la => localAuthority.StatisticalNeighbours != null && localAuthority.StatisticalNeighbours.All(n => n.Code != la.Code))
        .OrderBy(l => l.Name)
        .Select(ToCard)
        .ToArray();

    public LocalAuthority[] AvailableLocalAuthorities => allLocalAuthorities
        .Where(l => l.Code != Code)
        .Where(l => !Comparators.Contains(l.Code))
        .ToArray();

    private static RemovableItemCardViewModel ToCard(LocalAuthority la) =>
        new() { Title = la.Name, Identifier = la.Code };
}
