namespace Web.App.ViewModels.Components;

public class RagStackViewModel(string identifier, int red, int amber, int green, int height)
{
    private decimal Total => red + amber + green;
    public string Identifier => identifier;
    public decimal Red => red / Total * 100;
    public decimal Amber => amber / Total * 100;
    public decimal Green => green / Total * 100;
    public int Height => height;
}