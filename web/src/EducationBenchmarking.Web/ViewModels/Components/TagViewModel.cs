namespace EducationBenchmarking.Web.ViewModels.Components;

public class TagViewModel(TagColour colour, string displayText)
{
    public TagColour Colour => colour;
    public string DisplayText => displayText;
}