namespace Platform.Domain;

//TODO: Consider converting these to enums
public static class Dimensions
{
    public static class Census
    {
        public const string HeadcountPerFte = nameof(HeadcountPerFte);
        public const string Total = nameof(Total);
        public const string PercentWorkforce = nameof(PercentWorkforce);
        public const string PupilsPerStaffRole = nameof(PupilsPerStaffRole);

        public static readonly string[] All =
        [
            HeadcountPerFte,
            Total,
            PercentWorkforce,
            PupilsPerStaffRole
        ];

        public static bool IsValid(string? dimension) => All.Any(a => a == dimension);
    }

    public static class Finance
    {
        public const string Actuals = nameof(Actuals);
        public const string PerUnit = nameof(PerUnit);
        public const string PercentExpenditure = nameof(PercentExpenditure);
        public const string PercentIncome = nameof(PercentIncome);

        public static readonly string[] All =
        [
            Actuals,
            PerUnit,
            PercentExpenditure,
            PercentIncome
        ];

        public static bool IsValid(string? dimension) => All.Any(a => a == dimension);
    }

    public static class HighNeeds
    {
        public const string Actuals = nameof(Actuals);
        public const string PerHead = nameof(PerHead);

        public static readonly string[] All =
        [
            Actuals,
            PerHead
        ];

        public static bool IsValid(string? dimension) => All.Any(a => a == dimension);
    }

    public static class EducationHealthCarePlans
    {
        public const string Actuals = nameof(Actuals);
        public const string Per1000 = nameof(Per1000);

        public static readonly string[] All =
        [
            Actuals,
            Per1000
        ];

        public static bool IsValid(string? dimension) => All.Any(a => a == dimension);
    }
}