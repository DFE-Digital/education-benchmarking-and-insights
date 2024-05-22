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

    public IEnumerable<(string Category, int Red, int Amber, int Green)> Ratings => ratings
        .GroupBy(x => (x.Status, x.CostCategory))
        .Select(x => (x.Key.Status, x.Key.CostCategory, Count: x.Count()))
        .GroupBy(x => x.CostCategory)
        .Where(x => x.Key != "Other")
        .Select(x => (
            x.Key!,
            x.Where(r => r.Status == "Red").Select(r => r.Count).SingleOrDefault(),
            x.Where(a => a.Status == "Amber").Select(a => a.Count).SingleOrDefault(),
            x.Where(g => g.Status == "Green").Select(g => g.Count).SingleOrDefault()
            ))
        .OrderByDescending(x => x.Item2)
        .ThenByDescending(x => x.Item3);
}