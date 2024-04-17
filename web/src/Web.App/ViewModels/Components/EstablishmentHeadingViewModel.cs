namespace Web.App.ViewModels.Components;

public class EstablishmentHeadingViewModel(string title, string name, string id, string kind)
{
    public string Title => title;
    public string Name => name;
    public string Id => id;
    public string Kind => kind;
}