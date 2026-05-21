namespace Web.App.ViewModels.Components;

public class CustomDataBannerViewModel(string name, string urn)
{
    public string? Name => name;
    public string? Id => urn;
}
