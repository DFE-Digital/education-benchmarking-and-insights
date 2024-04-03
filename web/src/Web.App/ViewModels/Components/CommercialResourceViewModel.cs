namespace Web.App.ViewModels.Components;

public class CommercialResourceViewModel(string section, (string Title, string Link)[]? links)
{
    public string Section => section;
    public (string Title, string Url)[] Links => links ?? Array.Empty<(string, string)>();
}