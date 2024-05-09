namespace Web.App;

public static class SessionKeys
{
    public static string ComparatorSet(string urn) => $"comparator-set-{urn}";
    public static string CustomData(string urn) => $"custom-data-{urn}";
}