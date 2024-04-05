namespace Web.App.ViewModels.Components;

public class CommercialResourceViewModel(string section, (string Title, string Link)[]? links, bool displayHeading = true)
{
    public string Section => section;
    public bool DisplayHeading => displayHeading;
    public (string Title, string Url)[] Links => links ?? Array.Empty<(string, string)>();
}