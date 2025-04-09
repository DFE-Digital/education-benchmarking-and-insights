using Web.App.Domain;
namespace Web.App.ViewModels;

public class LocalAuthorityComparatorSetViewModel(LocalAuthority localAuthority, string[] strings)
{
    public string? Code => localAuthority.Code;
    public string? Name => localAuthority.Name;

}
