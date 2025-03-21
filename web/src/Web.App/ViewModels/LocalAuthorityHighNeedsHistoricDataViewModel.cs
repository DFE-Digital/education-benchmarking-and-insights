using Web.App.Domain;
using Web.App.Domain.LocalAuthorities;

namespace Web.App.ViewModels;

public class LocalAuthorityHighNeedsHistoricDataViewModel(LocalAuthority localAuthority, LocalAuthority<HighNeeds>[]? highNeeds)
{
    public bool HasHighNeeds = highNeeds?.Select(h => h.Outturn?.HighNeedsAmount).FirstOrDefault() != null;
    public string? Code => localAuthority.Code;
    public string? Name => localAuthority.Name;
}