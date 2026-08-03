using Web.App.Domain;

namespace Web.App.ViewModels;

public class LocalAuthorityRisksViewModel(LocalAuthority localAuthority, int cfrYear)
{
    public string? Code => localAuthority.Code;
    public string? Name => localAuthority.Name;
    public int? CfrYear => cfrYear;
}
