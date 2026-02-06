namespace Web.App.ViewModels;

public class RemovableItemCardGridViewModel
{
    public RemovableItemCardViewModel[] Cards { get; init; } = [];
    public string? InputName { get; init; }
    public string? Id { get; init; }
}

public class RemovableItemCardViewModel
{
    public string? Title { get; init; }
    public string? Identifier { get; init; }
}