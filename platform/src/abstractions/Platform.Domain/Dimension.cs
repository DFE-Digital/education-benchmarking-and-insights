namespace Platform.Domain;

public static class Dimension
{
    public static class Census
    {
        public const string HeadcountPerFte = nameof(HeadcountPerFte);
        public const string Total = nameof(Total);
        public const string PercentWorkforce = nameof(PercentWorkforce);
        public const string PupilsPerStaffRole = nameof(PupilsPerStaffRole);

        public static readonly string[] All =
        {
            HeadcountPerFte,
            Total,
            PercentWorkforce,
            PupilsPerStaffRole
        };

        public static bool IsValid(string? dimension) => All.Any(a => a == dimension);
    }
    
    public static class Cost
    {
        public const string Actuals = nameof(Actuals);
        public const string PerUnit = nameof(PerUnit);
        public const string PercentExpenditure = nameof(PercentExpenditure);
        public const string PercentIncome = nameof(PercentIncome);

        public static readonly string[] All =
        {
            Actuals,
            PerUnit,
            PercentExpenditure,
            PercentIncome
        };

        public static bool IsValid(string? dimension) => All.Any(a => a == dimension);
    }
}