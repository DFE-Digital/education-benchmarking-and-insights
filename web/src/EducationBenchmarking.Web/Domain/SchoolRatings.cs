using System.Diagnostics.CodeAnalysis;

namespace EducationBenchmarking.Web.Domain;

[ExcludeFromCodeCoverage]
public class SchoolRatings(Finances currentFinances, IEnumerable<Rating> ratings)
{
    public Finances CurrentFinances { get; set; } = currentFinances;
    public IEnumerable<Rating> Ratings { get; set; } = ratings;
}