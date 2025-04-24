// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Web.App.ViewModels.Search;

public class LocalAuthoritySearchViewModel : FindLocalAuthorityViewModel
{
    public string? Action { get; set; }
    public string? OrderBy { get; set; }
}