using Web.App.Domain;
namespace Web.App.ViewModels;

public class SchoolViewModel(School school)
{
    public SchoolViewModel(
        School school,
        SchoolBalance? balance,
        IEnumerable<RagRating> ratings,
        bool? comparatorGenerated = false,
        bool? comparatorReverted = false,
        string? userDefinedSetId = null,
        string? customDataId = null)
        : this(school)
    {
        UserDefinedSetId = userDefinedSetId;
        CustomDataId = customDataId;
        InYearBalance = balance?.InYearBalance;
        RevenueReserve = balance?.RevenueReserve;
        ComparatorGenerated = comparatorGenerated;
        ComparatorReverted = comparatorReverted;

        var ratingsArray = ratings.ToArray();

        HasMetricRag = ratingsArray.Length != 0;
        Ratings = ratingsArray
            .Where(x => x.RAG is "red" or "amber" && x.Category is not Category.Other)
            .OrderBy(x => Lookups.StatusOrderMap[x.RAG ?? string.Empty])
            .ThenByDescending(x => x.Decile)
            .ThenByDescending(x => x.Value)
            .Take(3);
    }

    public SchoolViewModel(
        School school,
        IEnumerable<RagRating> ratings)
        : this(school)
    {
        var ratingsArray = ratings.ToArray();

        HasMetricRag = ratingsArray.Length != 0;
        Ratings = ratingsArray
            .Where(x => x.RAG is "red" or "amber" && x.Category is not Category.Other)
            .OrderBy(x => Lookups.StatusOrderMap[x.RAG ?? string.Empty])
            .ThenByDescending(x => x.Decile)
            .ThenByDescending(x => x.Value);
    }

    public SchoolViewModel(
        School school,
        bool? customDataGenerated = false)
        : this(school)
    {
        CustomDataGenerated = customDataGenerated;
    }

    public string? Name => school.SchoolName;
    public string? Urn => school.URN;
    public string? OverallPhase => school.OverallPhase;
    public string? OfstedRating => school.OfstedDescription;
    public bool IsPartOfTrust => school.IsPartOfTrust;
    public string? Address => school.Address;
    public string? Telephone => school.Telephone;
    public string? LocalAuthorityName => school.LAName;
    public string? TrustIdentifier => school.TrustCompanyNumber;
    public string? TrustName => school.TrustName;

    public string Website
    {
        get
        {
            var url = school.Website;
            if (!string.IsNullOrEmpty(url) && !url.StartsWith("http", StringComparison.CurrentCultureIgnoreCase))
            {
                url = "http://" + url;
            }


            return Uri.IsWellFormedUriString(url, UriKind.Absolute) ? url : "";
        }
    }

    public string? UserDefinedSetId { get; }
    public string? CustomDataId { get; }
    public decimal? InYearBalance { get; }
    public decimal? RevenueReserve { get; }
    public bool HasMetricRag { get; }
    public IEnumerable<RagRating> Ratings { get; } = [];
    public bool? ComparatorGenerated { get; }
    public bool? ComparatorReverted { get; }
    public bool? CustomDataGenerated { get; }
}