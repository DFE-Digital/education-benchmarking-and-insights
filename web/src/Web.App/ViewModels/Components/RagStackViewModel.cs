namespace Web.App.ViewModels.Components;

public class RagStackViewModel(string identifier, int red, int amber, int green, int height)
{
    public string Identifier => identifier;

    public decimal Red => red;
    public decimal Amber => amber;
    public decimal Green => green;
    public decimal Total => red + amber + green;

    public decimal RedPercentage => Total > 0 ? red / Total * 100 : 0;
    public decimal AmberPercentage => Total > 0 ? amber / Total * 100 : 0;
    public decimal GreenPercentage => Total > 0 ? green / Total * 100 : 0;

    public string? RedHref { get; init; }
    public string? AmberHref { get; init; }
    public string? GreenHref { get; init; }

    public int Height => height;
}