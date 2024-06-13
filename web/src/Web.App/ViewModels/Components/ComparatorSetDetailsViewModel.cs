namespace Web.App.ViewModels.Components;

public class ComparatorSetDetailsViewModel(string identifier, bool hasUserDefinedSet, bool hasCustomData)
{
    public string Identifier => identifier;
    public bool HasUserDefinedSet => hasUserDefinedSet;
    public bool HasCustomData => hasCustomData;
}