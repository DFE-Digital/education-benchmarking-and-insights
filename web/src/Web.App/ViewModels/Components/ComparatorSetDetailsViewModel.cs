namespace Web.App.ViewModels.Components;

public class ComparatorSetDetailsViewModel(string identifier, bool hasUserDefinedSet)
{
    public string Identifier => identifier;
    public bool HasUserDefinedSet => hasUserDefinedSet;
}