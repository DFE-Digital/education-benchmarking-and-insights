using Web.App.Domain;

namespace Web.App.ViewModels;

public class TrustViewModel(Trust trust)
{
    public TrustViewModel(
        Trust trust,
        IReadOnlyCollection<School> schools)
        : this(trust)
    {
        Schools = schools;
    }

    public TrustViewModel(
        Trust trust,
        TrustBalance balance,
        IReadOnlyCollection<School> schools,
        IEnumerable<RagRating> ratings)
        : this(trust)
    {
        NumberSchools = schools.Count;
        RevenueReserve = balance.RevenueReserve;
        InYearBalance = balance.InYearBalance;

        var ratingsArray = ratings.ToArray();

        Low = ratingsArray.Where(NotOther).Count(Green);
        Medium = ratingsArray.Where(NotOther).Count(Amber);
        High = ratingsArray.Where(NotOther).Count(Red);
        Ratings = ratingsArray
            .Where(NotOther)
            .GroupBy(x => (x.RAG, x.Category))
            .Select(x => (x.Key.RAG, x.Key.Category, Count: x.Count()))
            .GroupBy(x => x.Category)
            .Select(x => new RagCostCategoryViewModel(
                x.Key,
                x.Where(w => Red(w.RAG)).Select(r => r.Count).SingleOrDefault(),
                x.Where(w => Amber(w.RAG)).Select(a => a.Count).SingleOrDefault(),
                x.Where(w => Green(w.RAG)).Select(g => g.Count).SingleOrDefault()
            ))
            .OrderByDescending(o => o.RedRatio)
            .ThenByDescending(o => o.AmberRatio)
            .ThenBy(o => o.Category);

        GroupedSchools = schools
            .GroupBy(x => x.OverallPhase)
            .Select(x => (
                OverallPhase: x.Key,
                Schools: x
                    .Select(s => new RagSchoolViewModel(
                        s.URN,
                        s.SchoolName,
                        ratingsArray.Where(NotOther).Where(Red).Count(r => r.URN == s.URN),
                        ratingsArray.Where(NotOther).Where(Amber).Count(r => r.URN == s.URN),
                        ratingsArray.Where(NotOther).Where(Green).Count(r => r.URN == s.URN)
                    ))
                    .OrderByDescending(o => o.RedRatio)
                    .ThenByDescending(o => o.AmberRatio)
                    .ThenBy(o => o.Name)
            ));
    }

    public string? CompanyNumber => trust.CompanyNumber;
    public string? Name => trust.TrustName;
    public string? Uid => trust.UID;
    public string? Contaact => trust.CFOEmail;
    public int NumberSchools { get; }
    public decimal? RevenueReserve { get; }
    public decimal? InYearBalance { get; }
    public int Low { get; }
    public int Medium { get; }
    public int High { get; }
    public IEnumerable<School> Schools { get; } = [];
    public IEnumerable<RagCostCategoryViewModel> Ratings { get; } = [];

    public IEnumerable<RagSchoolViewModel> PrimarySchools => GroupedSchools
        .Where(s => s.OverallPhase == OverallPhaseTypes.Primary)
        .SelectMany(s => s.Schools);

    public IEnumerable<RagSchoolViewModel> SecondarySchools => GroupedSchools
        .Where(s => s.OverallPhase == OverallPhaseTypes.Secondary)
        .SelectMany(s => s.Schools);

    public IEnumerable<RagSchoolViewModel> Special => GroupedSchools
        .Where(s => s.OverallPhase is OverallPhaseTypes.Special)
        .SelectMany(s => s.Schools);

    public IEnumerable<RagSchoolViewModel> AlternativeProvision => GroupedSchools
        .Where(s => s.OverallPhase is OverallPhaseTypes.AlternativeProvision)
        .SelectMany(s => s.Schools);

    public IEnumerable<RagSchoolViewModel> AllThroughSchools => GroupedSchools
        .Where(s => s.OverallPhase == OverallPhaseTypes.AllThrough)
        .SelectMany(s => s.Schools);

    public IEnumerable<RagSchoolViewModel> UniversityTechnicalColleges => GroupedSchools
        .Where(s => s.OverallPhase == OverallPhaseTypes.UniversityTechnicalCollege)
        .SelectMany(s => s.Schools);

    public IEnumerable<RagSchoolViewModel> PostSixteen => GroupedSchools
        .Where(s => s.OverallPhase == OverallPhaseTypes.PostSixteen)
        .SelectMany(s => s.Schools);



    private IEnumerable<(string? OverallPhase, IOrderedEnumerable<RagSchoolViewModel> Schools)> GroupedSchools { get; } = [];

    private static bool NotOther(RagRating ragRating) => ragRating.Category != Category.Other;
    private static bool Red(RagRating ragRating) => Red(ragRating.RAG);
    private static bool Amber(RagRating ragRating) => Amber(ragRating.RAG);
    private static bool Green(RagRating ragRating) => Green(ragRating.RAG);
    private static bool Red(string? status) => status == "red";
    private static bool Amber(string? status) => status == "amber";
    private static bool Green(string? status) => status == "green";
}

public class TrustSchoolsSectionViewModel
{
    public string Heading { get; init; } = string.Empty;
    public IEnumerable<RagSchoolViewModel> Schools { get; init; } = [];
}