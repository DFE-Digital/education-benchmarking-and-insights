namespace Web.App.ViewModels.Components;

public class ComparatorSetDetailsViewModel(string identifier, bool hasUserDefinedSet, bool hasCustomData, ComparatorSetType type)
{
    public string Identifier => identifier;
    public bool HasUserDefinedSet => hasUserDefinedSet;
    public bool HasCustomData => hasCustomData;
    public ComparatorSetType Type => type;
}

public enum ComparatorSetType
{
    Costs,
    Workforce
}

