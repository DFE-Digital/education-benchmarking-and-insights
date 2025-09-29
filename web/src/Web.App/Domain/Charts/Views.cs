namespace Web.App.Domain.Charts;

public static class Views
{
    public enum ViewAsOptions
    {
        Chart = 0,
        Table = 1
    }

    public static readonly ViewAsOptions[] All =
    [
        ViewAsOptions.Chart,
        ViewAsOptions.Table,
    ];

    public static string GetDescription(this ViewAsOptions option) => option switch
    {
        ViewAsOptions.Chart => "Chart",
        ViewAsOptions.Table => "Table",
        _ => throw new ArgumentException(nameof(option))
    };
}