using Web.App.Domain;

namespace Web.App.ViewModels.Components;

public class CommercialResourceViewModel(string section, IEnumerable<CommercialResourceLink> links, bool displayHeading = true)
{
    public string Section => section;
    public bool DisplayHeading => displayHeading;
    public IEnumerable<CommercialResourceLink> Links => links;
}