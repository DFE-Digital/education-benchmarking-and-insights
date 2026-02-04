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

    public LocalAuthority[] SelectedNeighbours => neighbourComparators
        .Join(localAuthorities, c => c, l => l.Code, (_, localAuthority) => localAuthority)
        .OrderBy(l => l.Name)
        .ToArray();

    public LocalAuthority[] SelectedOthers => otherComparators
        .Join(localAuthorities, c => c, l => l.Code, (_, localAuthority) => localAuthority)
        .OrderBy(l => l.Name)
        .ToArray();
}