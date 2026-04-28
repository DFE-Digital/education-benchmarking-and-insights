namespace Platform.Domain;

//TODO: Consider converting these to enums
public static class Dimensions
{
    public static class Census
    {
        /// <summary>The ratio of total headcount to full-time equivalent staff.</summary>
        public const string HeadcountPerFte = nameof(HeadcountPerFte);
        /// <summary>The absolute total number for the given category.</summary>
        public const string Total = nameof(Total);
        /// <summary>The proportion of the category relative to the entire workforce.</summary>
        public const string PercentWorkforce = nameof(PercentWorkforce);
        /// <summary>The ratio of total pupils to the specific staff role.</summary>
        public const string PupilsPerStaffRole = nameof(PupilsPerStaffRole);

        public static readonly string[] All = [
            HeadcountPerFte,
            Total,
            PercentWorkforce,
            PupilsPerStaffRole
        ];

        public static bool IsValid(string? dimension) => All.Any(a => a == dimension);
    }

    public static class Finance
    {
        /// <summary>Absolute monetary values in pounds.</summary>
        public const string Actuals = nameof(Actuals);
        /// <summary>Monetary values calculated per pupil or unit.</summary>
        public const string PerUnit = nameof(PerUnit);
        /// <summary>Values expressed as a percentage of total expenditure.</summary>
        public const string PercentExpenditure = nameof(PercentExpenditure);
        /// <summary>Values expressed as a percentage of total income.</summary>
        public const string PercentIncome = nameof(PercentIncome);

        public static readonly string[] All = [
            Actuals,
            PerUnit,
            PercentExpenditure,
            PercentIncome
        ];

        public static bool IsValid(string? dimension) => All.Any(a => a == dimension);
    }

    public static class HighNeeds
    {
        /// <summary>Absolute monetary values in pounds.</summary>
        public const string Actuals = nameof(Actuals);
        /// <summary>Monetary values calculated per head of population.</summary>
        public const string PerHead = nameof(PerHead);
        /// <summary>Monetary values calculated per pupil.</summary>
        public const string PerPupil = nameof(PerPupil);
        /// <summary>Monetary values calculated per EHC plan.</summary>
        public const string PerEhcp = nameof(PerEhcp);
        /// <summary>Monetary values calculated per SEN Support.</summary>
        public const string PerSenSupport = nameof(PerSenSupport);
        /// <summary>Monetary values calculated per EHC plan and SEN Support.</summary>
        public const string PerTotalSupport = nameof(PerTotalSupport);

        public static readonly string[] All =
        [
            Actuals,
            PerHead,
            PerPupil,
            PerEhcp,
            PerSenSupport,
            PerTotalSupport
        ];

        public static readonly string[] Population =
        [
            Actuals,
            PerHead,
            PerPupil
        ];

        public static readonly string[] Support =
        [
            PerPupil,
            PerEhcp,
            PerSenSupport,
            PerTotalSupport
        ];

        public static bool IsValid(string? dimension) => All.Any(a => a == dimension);
        public static bool IsValidPopulation(string? dimension) => Population.Any(a => a == dimension);
        public static bool IsValidSupport(string? dimension) => Support.Any(a => a == dimension);
    }

    public static class EducationHealthCarePlans
    {
        /// <summary>Absolute monetary values in pounds.</summary>
        public const string Actuals = nameof(Actuals);
        /// <summary>Values expressed per 1,000 of the relevant population.</summary>
        public const string Per1000 = nameof(Per1000);
        /// <summary>Values expressed per 1,000 pupils.</summary>
        public const string Per1000Pupil = nameof(Per1000Pupil);

        public static readonly string[] All =
        [
            Actuals,
            Per1000,
            Per1000Pupil
        ];

        public static bool IsValid(string? dimension) => All.Any(a => a == dimension);
    }

    public static class SchoolsSummaryWorkforce
    {
        /// <summary>Absolute monetary values in pounds.</summary>
        public const string Actuals = nameof(Actuals);
        /// <summary>Values expressed as a percentage relative to pupil numbers.</summary>
        public const string PercentPupil = nameof(PercentPupil);

        public static readonly string[] All =
        [
            Actuals,
            PercentPupil
        ];

        public static bool IsValid(string? dimension) => All.Any(a => a == dimension);
    }
}