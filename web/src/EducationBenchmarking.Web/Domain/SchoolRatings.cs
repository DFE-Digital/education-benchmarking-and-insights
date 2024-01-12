using System.Diagnostics.CodeAnalysis;

namespace EducationBenchmarking.Web.Domain;

[ExcludeFromCodeCoverage]
public class SchoolRatings
{
    public SchoolRatings(Finances currentFinances, IEnumerable<Rating> ratings)
    {
        CurrentFinances = currentFinances;
        Ratings = ratings;
    }

    public Finances CurrentFinances { get; set; }
    public IEnumerable<Rating> Ratings { get; set; }
}