using Web.App.Domain;

namespace Web.App.ViewModels;

public class TrustViewModel(Trust trust, IReadOnlyCollection<School> schools, IEnumerable<RagRating> ratings)
{
    public string? CompanyNumber => trust.CompanyNumber;
    public string? Name => trust.Name;
    public IEnumerable<School> Schools => schools;
    public int NumberSchools => schools.Count;
    public int Low => ratings.Count(x => x.Status == "Green");
    public int Medium => ratings.Count(x => x.Status == "Amber");
    public int High => ratings.Count(x => x.Status == "Red");

    public IEnumerable<(string? Status, string? Category, int Count)> Ratings => ratings
        .Where(x => x.Status is "Red" or "Amber")
        .GroupBy(x => (x.Status, x.StatusOrder, x.CostCategory))
        .OrderBy(x => x.Key.StatusOrder)
        .ThenByDescending(x => x.Count())
        .Select(x => (x.Key.Status, x.Key.CostCategory, x.Count()))
        .Take(3);
}