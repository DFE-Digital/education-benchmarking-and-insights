using Web.App.Domain;

namespace Web.App.ViewModels;

public class TrustViewModel(Trust trust, RagRating[] ratings)
{
    public string? CompanyNumber => trust.CompanyNumber;
    public string? Name => trust.Name;

    public int Low => ratings.Count(x => x.Status == "Green");
    public int Medium => ratings.Count(x => x.Status == "Amber");
    public int High => ratings.Count(x => x.Status == "Red");
}