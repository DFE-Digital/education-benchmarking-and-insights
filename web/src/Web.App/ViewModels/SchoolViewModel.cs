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
        PeriodCoveredByReturn = balance?.PeriodCoveredByReturn;
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
            .Where(x => x.RAG is "red" or "amber")
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

    public int? PeriodCoveredByReturn { get; }

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

    public Dictionary<string, string> Lookup1 => new()
    {
        { "Administrative supplies", "_AdministrativeSupplies" },
        { "Catering staff and supplies", "_CateringStaffServices" },
        { "Educational ICT", "_EducationalICT" },
        { "Educational supplies", "_EducationalSupplies" },
        { "Non-educational support staff", "_NonEducationalSupportStaff" },
        { "Other costs", "_OtherCosts" },
        { "Premises staff and services", "_PremisesStaffServices" },
        { "Teaching and Teaching support staff", "_TeachingStaff" },
        { "Utilities", "_Utilities" }
    };
    
    public Dictionary<string, bool> Lookup2 => new()
    {
        { "Administrative supplies", false },
        { "Catering staff and supplies", false },
        { "Educational ICT", false },
        { "Educational supplies", false },
        { "Non-educational support staff", false },
        { "Other costs", true },
        { "Premises staff and services", true },
        { "Teaching and Teaching support staff", true },
        { "Utilities", true }
    };

    
};
