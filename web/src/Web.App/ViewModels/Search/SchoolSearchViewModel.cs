// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Web.App.ViewModels.Search;

public class SchoolSearchViewModel : FindSchoolViewModel
{
    public string? Action { get; set; }
    public string? OrderBy { get; set; }
    public string[] OverallPhase { get; set; } = [];
}