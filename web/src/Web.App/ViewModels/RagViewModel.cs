namespace Web.App.ViewModels;

public abstract class RagViewModel(int red, int amber, int green)
{
    public int Red => red;
    public int Amber => amber;
    public int Green => green;
    public int Weighting => red * 25 + amber * 5 + green;
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