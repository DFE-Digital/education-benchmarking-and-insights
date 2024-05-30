using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolResourcesViewModel(School school, IEnumerable<RagRating> ratings)
{
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;
    public IEnumerable<RagRating> Ratings => ratings
        .Where(x => x.Status is "Red" or "Amber")
        .OrderBy(x => x.StatusOrder)
        .ThenByDescending(x => x.Decile)
        .ThenByDescending(x => x.Value);
}

public class ResourceViewModel(bool displayHeading = true)
{
    public bool DisplayHeading => displayHeading;
};