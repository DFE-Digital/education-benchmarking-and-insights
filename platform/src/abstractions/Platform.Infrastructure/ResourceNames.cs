using System.Diagnostics.CodeAnalysis;
namespace Platform.Infrastructure;

[ExcludeFromCodeCoverage]
public static class ResourceNames
{
    public static class Search
    {
        public static class DataSources
        {
            public const string School = "school-data-source";
            public const string Trust = "trust-data-source";
            public const string LocalAuthority = "local-authority-data-source";
            public const string SchoolComparators = "school-comparators-data-source";
            public const string TrustComparators = "trust-comparators-data-source";
        }

        public static class Indexers
        {
            public const string School = "school-indexer";
            public const string Trust = "trust-indexer";
            public const string LocalAuthority = "local-authority-indexer";
            public const string SchoolComparators = "school-comparators-indexer";
            public const string TrustComparators = "trust-comparators-indexer";

            public static readonly string[] All = [School, Trust, LocalAuthority, SchoolComparators, TrustComparators];
        }

        public static class Indexes
        {
            public const string School = "school-index";
            public const string Trust = "trust-index";
            public const string LocalAuthority = "local-authority-index";
            public const string SchoolComparators = "school-comparators-index";
            public const string TrustComparators = "trust-comparators-index";

            public static readonly string[] All = [School, Trust, LocalAuthority, SchoolComparators, TrustComparators];
        }

        public static class Suggesters
        {
            public const string School = "school-suggester";
            public const string Trust = "trust-suggester";
            public const string LocalAuthority = "local-authority-suggester";
        }
    }
}