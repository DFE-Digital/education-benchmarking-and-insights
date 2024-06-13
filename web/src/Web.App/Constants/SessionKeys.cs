namespace Web.App;

public static class SessionKeys
{
    public static string ComparatorSet(string urn) => $"comparator-set-{urn}";
    public static string ComparatorSetCharacteristic(string urn) => $"comparator-set-characteristic-{urn}";
    public static string ComparatorSetUserDefined(string urn) => $"comparator-set-user-defined-{urn}";
    public static string ComparatorSetUserDefined(string urn, string identifier) => $"comparator-set-user-defined-{urn}-{identifier}";
    public static string CustomData(string urn) => $"custom-data-{urn}";
    public static string TrustComparatorSetCharacteristic(string urn) => $"trust-comparator-set-characteristic-{urn}";
    public static string TrustComparatorSetUserDefined(string companyNumber) => $"trust-comparator-set-user-defined-{companyNumber}";
    public static string TrustComparatorSetUserDefined(string companyNumber, string identifier) => $"trust-comparator-set-user-defined-{companyNumber}-{identifier}";
}