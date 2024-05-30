namespace Web.App.ViewModels;

public abstract class RagViewModel(int red, int amber, int green)
{
    public int Red => red;
    public int Amber => amber;
    public int Green => green;
    public decimal RedRatio => Total > 0 ? red / Total : 0;
    public decimal AmberRatio => Total > 0 ? amber / Total : 0;
    private decimal Total => red + amber + green;
}

public class RagCostCategoryViewModel(string? category, int red, int amber, int green) : RagViewModel(red, amber, green)
{
    public string? Category => category;
}

public class RagSchoolViewModel(string? urn, string? name, int red, int amber, int green) : RagViewModel(red, amber, green)
{
    public string? Urn => urn;
    public string? Name => name;
}