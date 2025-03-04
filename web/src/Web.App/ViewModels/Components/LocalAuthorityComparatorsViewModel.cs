using Web.App.Domain;

namespace Web.App.ViewModels.Components;

public class LocalAuthorityComparatorsViewModel(
    string code,
    IEnumerable<LocalAuthority> localAuthorities,
    string[] comparators)
{
    public string Code => code;

    public LocalAuthority[] AvailableLocalAuthorities => localAuthorities
        .Where(l => l.Code != code)
        .Where(l => !comparators.Contains(l.Code))
        .ToArray();

    public LocalAuthority[] SelectedLocalAuthorities => comparators
        .Join(localAuthorities, c => c, l => l.Code, (_, localAuthority) => localAuthority)
        .OrderBy(l => l.Name)
        .ToArray();
}