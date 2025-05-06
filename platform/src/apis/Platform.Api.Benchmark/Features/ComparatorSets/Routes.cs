namespace Platform.Api.Benchmark.Features.ComparatorSets;

public static class Routes
{
    public const string SchoolCustomComparatorSet = "comparator-set/school/{urn}/custom/{identifier}";
    public const string SchoolDefaultComparatorSet = "comparator-set/school/{urn}/default";
    public const string SchoolUserDefinedComparatorSet = "comparator-set/school/{urn}/user-defined";
    public const string SchoolUserDefinedComparatorSetItem = "comparator-set/school/{urn}/user-defined/{identifier}";
    public const string TrustUserDefinedComparatorSet = "comparator-set/trust/{companyNumber}/user-defined";
    public const string TrustUserDefinedComparatorSetItem = "comparator-set/trust/{companyNumber}/user-defined/{identifier}";
}