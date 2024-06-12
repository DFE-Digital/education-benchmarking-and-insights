namespace Web.App.ViewModels;

public record DataSourceViewModel(
    IEnumerable<DataSourceFileViewModel> Academies,
    IEnumerable<DataSourceFileViewModel> MaintainedSchools)
{
    public IEnumerable<DataSourceFileViewModel> Academies { get; set; } = Academies;
    public IEnumerable<DataSourceFileViewModel> MaintainedSchools { get; set; } = MaintainedSchools;
}

public record DataSourceFileViewModel
{
    public string? DisplayText { get; set; }
    public Uri? Link { get; set; }
}