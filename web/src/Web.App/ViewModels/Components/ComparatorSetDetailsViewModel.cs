namespace Web.App.ViewModels.Components;

public class ComparatorSetDetailsViewModel(string identifier, bool hasUserDefinedSet, bool hasCustomData, bool hasMissingComparatorSet, ComparatorSetType type, bool showCustomDataOption)
{
    public string Identifier => identifier;
    public bool HasUserDefinedSet => hasUserDefinedSet;
    public bool HasCustomData => hasCustomData;
    public bool HasMissingComparatorSet => hasMissingComparatorSet;
    public ComparatorSetType Type => type;
    public bool ShowCustomDataOption => showCustomDataOption;
}

public enum ComparatorSetType
{
    Costs,
    Workforce
}