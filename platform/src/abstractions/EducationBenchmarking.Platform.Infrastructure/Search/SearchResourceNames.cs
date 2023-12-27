namespace EducationBenchmarking.Platform.Infrastructure.Search;

public static class SearchResourceNames
{
    public static class DataSources
    {
        public const string School ="school-data-source";
        public const string Trust ="trust-data-source";
        
        public const string EstablishmentSchool ="estblishment-school-data-source";
        public const string EstablishmentTrust ="estblishment-trust-data-source";
        public const string EstablishmentLa ="estblishment-la-data-source";
    }
    
    public static class Indexers
    {
        public const string School ="school-indexer";
        public const string Trust ="trust-indexer";
        
        public const string EstablishmentSchool ="establishment-school-indexer";
        public const string EstablishmentTrust ="establishment-trust-indexer";
        public const string EstablishmentLa ="establishment-la-indexer";
    }
    
    public static class Indexes
    {
        public const string Establishment = "establishment-index";
        public const string School = "school-index";
        public const string Trust = "trust-index";
    }
    
    public static class Suggesters
    {
        public const string Trust = "trust-suggester";
        public const string School = "school-suggester";
        public const string Establishment = "establishment-suggester";
    }
}