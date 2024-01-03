using System.Diagnostics.CodeAnalysis;

namespace EducationBenchmarking.Platform.Infrastructure.Search;

[ExcludeFromCodeCoverage]
public static class SearchResourceNames
{
    public static class DataSources
    {
        public const string School = "school-data-source";
        public const string Trust = "trust-data-source";

        public const string OrganisationSchool = "organisation-school-data-source";
        public const string OrganisationTrust = "organisation-trust-data-source";
        public const string OrganisationLa = "organisation-la-data-source";
    }

    public static class Indexers
    {
        public const string School = "school-indexer";
        public const string Trust = "trust-indexer";

        public const string OrganisationSchool = "organisation-school-indexer";
        public const string OrganisationTrust = "organisation-trust-indexer";
        public const string OrganisationLa = "organisation-la-indexer";
    }

    public static class Indexes
    {
        public const string Organisation = "organisation-index";
        public const string School = "school-index";
        public const string Trust = "trust-index";
    }

    public static class Suggesters
    {
        public const string Trust = "trust-suggester";
        public const string School = "school-suggester";
        public const string Organisation = "organisation-suggester";
    }
}