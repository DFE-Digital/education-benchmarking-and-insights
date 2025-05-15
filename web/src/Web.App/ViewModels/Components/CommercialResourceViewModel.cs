using Web.App.Domain;

namespace Web.App.ViewModels.Components;

public class CommercialResourceViewModel(string section, List<LinkItem> links, bool displayHeading = true)
{
    public string Section => section;
    public bool DisplayHeading => displayHeading;
    public List<LinkItem> Links => links;
}