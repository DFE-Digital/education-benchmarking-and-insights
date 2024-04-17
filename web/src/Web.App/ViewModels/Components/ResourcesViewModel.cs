namespace Web.App.ViewModels.Components;

public class ResourcesViewModel(string identifier, IEnumerable<Resources> resources)
{
    public IEnumerable<Resources> Resources => resources;
    public string Identifier => identifier;
}

public enum Resources
{
    SchoolResources,
    SchoolHistoricData,
    TrustResources,
    TrustHistoricData,
    SchoolDetails
}