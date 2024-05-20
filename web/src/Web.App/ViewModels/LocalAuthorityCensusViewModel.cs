using Web.App.Domain;

namespace Web.App.ViewModels;

public class LocalAuthorityCensusViewModel(LocalAuthority localAuthority, string[] phases)
{
    public string? Code => localAuthority.Code;
    public string? Name => localAuthority.Name;
    public string[] Phases => phases;
}