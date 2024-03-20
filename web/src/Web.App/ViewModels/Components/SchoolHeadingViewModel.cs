namespace Web.App.ViewModels.Components;

public class SchoolHeadingViewModel(string pageTitle, string schoolName, string urn)
{
    public string PageTitle => pageTitle;
    public string SchoolName => schoolName;
    public string Urn => urn;
}