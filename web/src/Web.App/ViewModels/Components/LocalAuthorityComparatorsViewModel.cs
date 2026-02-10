using Web.App.Domain;

namespace Web.App.ViewModels.Components;

public class LocalAuthorityComparatorsViewModel(
    string code,
    IEnumerable<LocalAuthority> localAuthorities,
    string[] comparators,
    string[] neighbourComparators,
    string[] otherComparators)
{
    public string Code => code;

    public LocalAuthority[] AvailableLocalAuthorities => localAuthorities
        .Where(l => l.Code != code)
        .Where(l => !comparators.Contains(l.Code))
        .ToArray();

    public RemovableItemCardViewModel[] SelectedNeighbours => neighbourComparators
        .Join(localAuthorities, c => c, l => l.Code, (_, la) => la)
        .Select(ToCard)
        .ToArray();

    public RemovableItemCardViewModel[] SelectedOthers => otherComparators
        .Join(localAuthorities, c => c, l => l.Code, (_, la) => la)
        .OrderBy(l => l.Name)
        .Select(ToCard)
        .ToArray();

    private static RemovableItemCardViewModel ToCard(LocalAuthority la) =>
        new() { Title = la.Name, Identifier = la.Code };
}