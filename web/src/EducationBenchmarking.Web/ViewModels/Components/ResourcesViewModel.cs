namespace EducationBenchmarking.Web.ViewModels.Components;

public class ResourcesViewModel(string identifier, IEnumerable<Resources> resources)
{
    public IEnumerable<Resources> Resources => resources;
    public string Identifier => identifier;
}

public enum Resources
{
    FindCommercialResources,
    HistoricData,
    SchoolDetails
}