using Web.App.Domain;
using Web.App.Domain.Content;

namespace Web.App.ViewModels;

public class LocalAuthorityHighNeedsViewModel(LocalAuthority localAuthority, FinanceYears years)
{
    public string? Code => localAuthority.Code;
    public string? Name => localAuthority.Name;
    public int? Section251Year => years.S251;
}