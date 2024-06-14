namespace Web.App.ViewModels.Components;

public class TagViewModel(TagColour? colour, string? displayText)
{
    public TagColour Colour => colour ?? TagColour.Grey;
    public string DisplayText => displayText ?? "";
}