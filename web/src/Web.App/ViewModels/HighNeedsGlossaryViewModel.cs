namespace Web.App.ViewModels;

public class HighNeedsGlossaryViewModel
{
    public HighNeedsGlossaryItem[] Glossary =>
    [
        new()
        {
            Term = "AAR",
            Meaning = "Academy Accounts Return"
        }
    ];
}

public class HighNeedsGlossaryItem
{
    public string? Term { get; init; }
    public string? Meaning { get; init; }
}