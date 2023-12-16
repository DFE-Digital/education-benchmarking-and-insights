namespace EducationBenchmarking.Platform.Search;

public static class Names
{
    public static class DataSources
    {
        public const string CosmosEbisEdubase ="cosmos-ebis-edubase";
        public const string CosmosEbisEdubaseSchool ="cosmos-ebis-edubase-school";
    }
    
    public static class Indexers
    {
        public const string CosmosSchoolEdubase ="school-edubase-cosmos-indexer";
        public const string CosmosEstablishmentSchoolEdubase ="establishment-school-edubase-cosmos-indexer";
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