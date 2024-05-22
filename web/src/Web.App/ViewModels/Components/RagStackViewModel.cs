namespace Web.App.ViewModels.Components;

public class RagStackViewModel(string identifier, int red, int amber, int green, int height)
{
    public string Identifier => identifier;

    public decimal Red => red;
    public decimal Amber => amber;
    public decimal Green => green;
    private decimal Total => red + amber + green;

    public decimal RedPercentage => red / Total * 100;
    public decimal AmberPercentage => amber / Total * 100;
    public decimal GreenPercentage => green / Total * 100;

    public string? RedHref { get; init; }
    public string? AmberHref { get; init; }
    public string? GreenHref { get; init; }

    public int Height => height;
}