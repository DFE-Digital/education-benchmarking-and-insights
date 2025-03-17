namespace Platform.Domain;

//TODO: Consider converting these to enums
public static class Ranking
{
    public static class LocalAuthorityNationalRanking
    {
        public const string SpendAsPercentageOfBudget = nameof(SpendAsPercentageOfBudget);

        public static readonly string[] All =
        [
            SpendAsPercentageOfBudget,
        ];

        public static bool IsValid(string? ranking) => All.Any(a => a == ranking);
    }

    public static class Sort
    {
        public const string Asc = nameof(Asc);
        public const string Desc = nameof(Desc);

        public static readonly string[] All =
        [
            Asc.ToLower(),
            Desc.ToLower()
        ];

        public static bool IsValid(string? order) => All.Any(a => a.Equals(order, StringComparison.OrdinalIgnoreCase));
    }
}