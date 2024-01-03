namespace EducationBenchmarking.Web.Domain;

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